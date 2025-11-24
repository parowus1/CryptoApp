using System;
using System.Windows.Forms;
using CryptoApp.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Linq;

namespace CryptoApp
{
    public partial class MainForm : Form
    {
        private readonly List<ICipher> _algorithms = new List<ICipher>();
        private ICipher? _currentCipher;

        public enum OperationMode { Encrypt, Decrypt }

        public MainForm()
        {
            InitializeComponent();

            _algorithms.Add(new RSACipher());
            _algorithms.Add(new AESCipher());
            _algorithms.Add(new RunningKeyCipher());
            _algorithms.Add(new VigenereCipher());
            _algorithms.Add(new CaesarCipher());

            if (cmbAlgorithms != null)
            {
                cmbAlgorithms.DataSource = _algorithms;
                cmbAlgorithms.DisplayMember = "Name";

                _currentCipher = _algorithms[0];
                cmbAlgorithms.SelectedIndex = 0;

                cmbAlgorithms.SelectedIndexChanged += CmbAlgorithms_SelectedIndexChanged;
            }
            else
            {
                MessageBox.Show("Błąd: Nie znaleziono kontrolki cmbAlgorithms. Sprawdź Projektanta.", "Błąd Konfiguracji UI", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (txtKey != null)
            {
                txtKey.Text = "SuperBezpieczneHaslo123";
            }
            if (lblCurrentCipher != null)
            {
                lblCurrentCipher.Text = $"Aktywny Algorytm: {_currentCipher.Name}";
            }

        }

        // --- Metoda obsługująca zmianę wybranego algorytmu ---
        private void CmbAlgorithms_SelectedIndexChanged(object? sender, EventArgs e)
        {
            ComboBox cmb = sender as ComboBox;

            // Ustawienie _currentCipher na wybrany element
            if (cmb.SelectedItem is ICipher selectedCipher)
            {
                _currentCipher = selectedCipher;

                bool isRsa = _currentCipher.Name.Contains("RSA");
                if (btnGenerateKeys != null) 
                {
                    btnGenerateKeys.Visible = isRsa;
                }

                if (lblCurrentCipher != null)
                {
                    lblCurrentCipher.Text = $"Aktywny Algorytm: {_currentCipher.Name}";
                }

                if (txtKey != null)
                {
                    if (_currentCipher.Name.Contains("Cezara"))
                    {
                        txtKey.Text = "3";
                    }
                    else if (_currentCipher.Name.Contains("Vigenère'a"))
                    {
                        txtKey.Text = "SECRET";
                    }
                    else if (_currentCipher.Name.Contains("Bieżącym"))
                    {
                        txtKey.Text = "DŁUGI KLUCZ O DŁUGOŚCI WIADOMOŚCI";
                    }
                    if (isRsa) // NOWA OBSŁUGA
                    {
                        txtKey.Text = "KLUCZ PUBLICZNY lub PRYWATNY (XML)";
                    }
                    else if (_currentCipher.Name.Contains("AES"))
                    {
                        txtKey.Text = "SuperBezpieczneHaslo123";
                    }
                }
            }
        }

        // --- Obsługa szyfrowania/deszyfrowania tekstu ---
        private void btnEncryptText_Click(object sender, EventArgs e)
        {
            try
            {
                string plainText = txtPlainText.Text;
                string key = txtKey.Text;

                txtCipherText.Text = _currentCipher.EncryptText(plainText, key);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Błąd klucza", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDecryptText_Click(object sender, EventArgs e)
        {
            try
            {
                string cipherText = txtCipherText.Text;
                string key = txtKey.Text;

                txtPlainText.Text = _currentCipher.DecryptText(cipherText, key);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Błąd klucza", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- Obsługa szyfrowania/deszyfrowania plików ---
        private async void btnEncryptFile_Click(object sender, EventArgs e)
        {
            await ProcessFileAsync(OperationMode.Encrypt);
        }

        private async void btnDecryptFile_Click(object sender, EventArgs e)
        {
            await ProcessFileAsync(OperationMode.Decrypt);
        }

        private async Task ProcessFileAsync(OperationMode mode)
        {
            using (var openDlg = new OpenFileDialog())
            using (var saveDlg = new SaveFileDialog())
            {
                if (openDlg.ShowDialog() != DialogResult.OK) return;

                // Używamy Path.GetFileName, by uzyskać nazwę pliku
                saveDlg.FileName = $"output_{mode}_{Path.GetFileName(openDlg.FileName)}";
                if (saveDlg.ShowDialog() != DialogResult.OK) return;

                // Sprawdzenie, czy etykieta statusu pliku istnieje
                if (lblFileStatus != null)
                {
                    lblFileStatus.Text = $"{mode}owanie pliku... Czekaj...";
                }

                string key = txtKey.Text;

                try
                {
                    if (mode == OperationMode.Encrypt)
                    {
                        await _currentCipher.EncryptFileAsync(openDlg.FileName, saveDlg.FileName, key);
                    }
                    else
                    {
                        await _currentCipher.DecryptFileAsync(openDlg.FileName, saveDlg.FileName, key);
                    }

                    if (lblFileStatus != null)
                    {
                        lblFileStatus.Text = $"{mode}owanie pliku zakończone pomyślnie!";
                    }
                }
                catch (ArgumentException ex)
                {
                    if (lblFileStatus != null) lblFileStatus.Text = "Błąd klucza!";
                    MessageBox.Show(ex.Message, "Błąd klucza", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    if (lblFileStatus != null) lblFileStatus.Text = "Wystąpił błąd podczas operacji na pliku.";
                    MessageBox.Show($"Błąd: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnGenerateKeys_Click(object sender, EventArgs e)
        {
            if (_currentCipher.Name.Contains("RSA"))
            {
                try
                {
                    var (publicKey, privateKey) = RSACipher.GenerateKeys();

                    // Krok 1: Wyświetlenie kluczy i instrukcja
                    MessageBox.Show(
                        "Klucze RSA wygenerowane pomyślnie!\n\n" +
                        "Klucz Publiczny (do szyfrowania) został skopiowany do głównego pola Klucza.\n\n" +
                        "Klucz Prywatny (do deszyfrowania) został skopiowany do schowka. NIE UDOSTĘPNIAJ GO!",
                        "Generowanie Kluczy RSA",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    // Krok 2: Ustawienie klucza publicznego w polu Klucza
                    txtKey.Text = publicKey;

                    // Krok 3: Skopiowanie klucza prywatnego do schowka (bezpieczniejsze niż wyświetlanie)
                    Clipboard.SetText(privateKey);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Błąd podczas generowania kluczy RSA: {ex.Message}", "Błąd RSA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Funkcja generowania kluczy dotyczy tylko Szyfru RSA.", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Puste metody są bezpieczne do usunięcia, ale jeśli Projektant je wygenerował, 
        // to powinny zostać, albo musisz sprawdzić, czy nie są gdzieś podpięte.
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}