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
        private LogWindow logWindow;

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
                txtKey.Text = "KLUCZ PUBLICZNY lub PRYWATNY (XML)";
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
                    else if (isRsa) 
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
            if (logWindow != null) logWindow.RefreshLogs();

            string key = txtKey.Text;
            try
            {
                string plainText = txtPlainText.Text;
                txtCipherText.Text = _currentCipher.EncryptText(plainText, key);

                LogOperation("EncryptText", true, "Tekst zaszyfrowany pomyślnie.", key);
            }
            catch (ArgumentException ex)
            {
                LogOperation("EncryptText", false, $"Błąd klucza: {ex.Message}", key);
                MessageBox.Show(ex.Message, "Błąd klucza", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                LogOperation("EncryptText", false, $"Wystąpił nieznany błąd: {ex.Message}", key);
                MessageBox.Show($"Wystąpił błąd: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDecryptText_Click(object sender, EventArgs e)
        {
            if (logWindow != null) logWindow.RefreshLogs();

            string key = txtKey.Text;
            try
            {
                string cipherText = txtCipherText.Text;
                txtPlainText.Text = _currentCipher.DecryptText(cipherText, key);

                LogOperation("DecryptText", true, "Tekst odszyfrowany pomyślnie.", key);
            }
            catch (ArgumentException ex)
            {
                LogOperation("DecryptText", false, $"Błąd klucza: {ex.Message}", key);
                MessageBox.Show(ex.Message, "Błąd klucza", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                LogOperation("DecryptText", false, $"Wystąpił nieznany błąd: {ex.Message}", key);
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

                    MessageBox.Show(
                        "Klucze RSA wygenerowane pomyślnie!\n\n" +
                        "Klucz Publiczny (do szyfrowania) został skopiowany do głównego pola Klucza.\n\n" +
                        "Klucz Prywatny (do deszyfrowania) został skopiowany do schowka. NIE UDOSTĘPNIAJ GO!",
                        "Generowanie Kluczy RSA",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    txtKey.Text = publicKey;

                    Clipboard.SetText(privateKey);
                    LogOperation("GenerateKeys", true, "Wygenerowano nową parę kluczy RSA-2048.", "Klucz Prywatny w Schowku");
                }
                catch (Exception ex)
                {
                    LogOperation("GenerateKeys", false, $"Błąd: {ex.Message}", "N/A");
                    MessageBox.Show($"Błąd podczas generowania kluczy RSA: {ex.Message}", "Błąd RSA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Funkcja generowania kluczy dotyczy tylko Szyfru RSA.", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void LogOperation(string operation, bool success, string message, string key = "")
        {
            string status = success ? "SUCCESS" : "ERROR";
            string algorithmName = _currentCipher?.Name ?? "N/A";

            LogManager.AddLog(algorithmName, operation, message, status, key);

        }

        private void BtnShowLogs_Click(object sender, EventArgs e)
        {
            if (logWindow == null || logWindow.IsDisposed)
            {
                logWindow = new LogWindow();
                logWindow.Show();
            }
            else
            {
                logWindow.BringToFront();
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}