using System;
using System.Windows.Forms;
using CryptoApp.Core; // Upewnij si�, �e masz to u�ycie
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CryptoApp
{
    public partial class MainForm : Form
    {
        // Lista dost�pnych algorytm�w (�atwe dodawanie nowych w przysz�o�ci)
        private readonly List<ICipher> _algorithms = new List<ICipher>();

        // Aktualnie wybrany algorytm
        private ICipher _currentCipher;

        public MainForm()
        {
            InitializeComponent();

            // 1. Inicjalizacja algorytm�w
            _algorithms.Add(new CaesarCipher());

            // 2. Wyb�r domy�lnego algorytmu
            _currentCipher = _algorithms[0];

            // 3. Konfiguracja ComboBox (je�li dodasz wi�cej algorytm�w)
            // Przyk�ad dla przysz�o�ci:
            // cmbAlgorithms.DataSource = _algorithms;
            // cmbAlgorithms.DisplayMember = "Name"; 
            // cmbAlgorithms.SelectedIndexChanged += CmbAlgorithms_SelectedIndexChanged;
        }

        // --- Obs�uga szyfrowania/deszyfrowania tekstu ---
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
                MessageBox.Show(ex.Message, "B��d klucza", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wyst�pi� b��d: {ex.Message}", "B��d", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(ex.Message, "B��d klucza", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wyst�pi� b��d: {ex.Message}", "B��d", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- Obs�uga szyfrowania/deszyfrowania plik�w ---
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

                saveDlg.FileName = $"output_{mode}_{Path.GetFileName(openDlg.FileName)}";
                if (saveDlg.ShowDialog() != DialogResult.OK) return;

                lblFileStatus.Text = $"{mode}owanie pliku... Czekaj...";
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

                    lblFileStatus.Text = $"{mode}owanie pliku zako�czone pomy�lnie!";
                }
                catch (ArgumentException ex)
                {
                    lblFileStatus.Text = "B��d klucza!";
                    MessageBox.Show(ex.Message, "B��d klucza", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    lblFileStatus.Text = "Wyst�pi� b��d podczas operacji na pliku.";
                    MessageBox.Show($"B��d: {ex.Message}", "B��d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }

    // Dodatkowy enum dla czytelno�ci kodu
    public enum OperationMode { Encrypt, Decrypt }
}