namespace CryptoApp
{
    partial class MainForm
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Czyści wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać usunięte; Fałsz w przeciwnym razie.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta Formularzy Systemu Windows

        /// <summary>
        /// Wymagana metoda wsparcia projektanta – nie modyfikować
        /// zawartość tej metody z edytorem kodu.
        /// </summary>
        private void InitializeComponent()
        {
            txtPlainText = new TextBox();
            txtCipherText = new TextBox();
            lblPlainText = new Label();
            lblCipherText = new Label();
            btnEncryptFile = new Button();
            btnDecryptFile = new Button();
            lblFileStatus = new Label();
            lblKey = new Label();
            txtKey = new TextBox();
            btnDecryptText = new Button();
            btnEncryptText = new Button();
            cmbAlgorithms = new ComboBox();
            btnGenerateKeys = new Button();
            btnShowLogs = new Button();
            SuspendLayout();
            // 
            // txtPlainText
            // 
            txtPlainText.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtPlainText.Location = new Point(10, 36);
            txtPlainText.Multiline = true;
            txtPlainText.Name = "txtPlainText";
            txtPlainText.ScrollBars = ScrollBars.Vertical;
            txtPlainText.Size = new Size(680, 113);
            txtPlainText.TabIndex = 0;
            // 
            // txtCipherText
            // 
            txtCipherText.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtCipherText.Location = new Point(10, 204);
            txtCipherText.Multiline = true;
            txtCipherText.Name = "txtCipherText";
            txtCipherText.ScrollBars = ScrollBars.Vertical;
            txtCipherText.Size = new Size(680, 113);
            txtCipherText.TabIndex = 1;
            // 
            // lblPlainText
            // 
            lblPlainText.AutoSize = true;
            lblPlainText.Location = new Point(10, 17);
            lblPlainText.Name = "lblPlainText";
            lblPlainText.Size = new Size(72, 15);
            lblPlainText.TabIndex = 4;
            lblPlainText.Text = "Tekst Jawny:";
            // 
            // lblCipherText
            // 
            lblCipherText.AutoSize = true;
            lblCipherText.Location = new Point(10, 186);
            lblCipherText.Name = "lblCipherText";
            lblCipherText.Size = new Size(112, 15);
            lblCipherText.TabIndex = 5;
            lblCipherText.Text = "Tekst Zaszyfrowany:";
            // 
            // btnEncryptFile
            // 
            btnEncryptFile.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnEncryptFile.Location = new Point(10, 339);
            btnEncryptFile.Name = "btnEncryptFile";
            btnEncryptFile.Size = new Size(105, 27);
            btnEncryptFile.TabIndex = 8;
            btnEncryptFile.Text = "Szyfruj Plik";
            btnEncryptFile.UseVisualStyleBackColor = true;
            btnEncryptFile.Click += btnEncryptFile_Click;
            // 
            // btnDecryptFile
            // 
            btnDecryptFile.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnDecryptFile.Location = new Point(121, 339);
            btnDecryptFile.Name = "btnDecryptFile";
            btnDecryptFile.Size = new Size(105, 27);
            btnDecryptFile.TabIndex = 9;
            btnDecryptFile.Text = "Deszyfruj Plik";
            btnDecryptFile.UseVisualStyleBackColor = true;
            btnDecryptFile.Click += btnDecryptFile_Click;
            // 
            // lblFileStatus
            // 
            lblFileStatus.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblFileStatus.AutoSize = true;
            lblFileStatus.Location = new Point(236, 346);
            lblFileStatus.Name = "lblFileStatus";
            lblFileStatus.Size = new Size(86, 15);
            lblFileStatus.TabIndex = 10;
            lblFileStatus.Text = "Status: Gotowy";
            // 
            // lblKey
            // 
            lblKey.AutoSize = true;
            lblKey.Location = new Point(185, 162);
            lblKey.Name = "lblKey";
            lblKey.Size = new Size(41, 15);
            lblKey.TabIndex = 15;
            lblKey.Text = "Klucz: ";
            // 
            // txtKey
            // 
            txtKey.Location = new Point(227, 159);
            txtKey.Name = "txtKey";
            txtKey.Size = new Size(73, 23);
            txtKey.TabIndex = 14;
            txtKey.Text = "3";
            // 
            // btnDecryptText
            // 
            btnDecryptText.Location = new Point(415, 155);
            btnDecryptText.Name = "btnDecryptText";
            btnDecryptText.Size = new Size(105, 27);
            btnDecryptText.TabIndex = 13;
            btnDecryptText.Text = "Deszyfruj Tekst";
            btnDecryptText.UseVisualStyleBackColor = true;
            btnDecryptText.Click += btnDecryptText_Click;
            // 
            // btnEncryptText
            // 
            btnEncryptText.Location = new Point(305, 155);
            btnEncryptText.Name = "btnEncryptText";
            btnEncryptText.Size = new Size(105, 27);
            btnEncryptText.TabIndex = 12;
            btnEncryptText.Text = "Szyfruj Tekst";
            btnEncryptText.UseVisualStyleBackColor = true;
            btnEncryptText.Click += btnEncryptText_Click;
            // 
            // cmbAlgorithms
            // 
            cmbAlgorithms.FormattingEnabled = true;
            cmbAlgorithms.Location = new Point(10, 159);
            cmbAlgorithms.Name = "cmbAlgorithms";
            cmbAlgorithms.Size = new Size(169, 23);
            cmbAlgorithms.TabIndex = 11;
            cmbAlgorithms.Tag = "lblCurrentCipher";
            cmbAlgorithms.Click += CmbAlgorithms_SelectedIndexChanged;
            // 
            // btnGenerateKeys
            // 
            btnGenerateKeys.Location = new Point(526, 156);
            btnGenerateKeys.Name = "btnGenerateKeys";
            btnGenerateKeys.Size = new Size(105, 27);
            btnGenerateKeys.TabIndex = 16;
            btnGenerateKeys.Text = "Generuj klucze";
            btnGenerateKeys.UseVisualStyleBackColor = true;
            btnGenerateKeys.Click += BtnGenerateKeys_Click;
            // 
            // btnShowLogs
            // 
            btnShowLogs.Location = new Point(612, 343);
            btnShowLogs.Name = "btnShowLogs";
            btnShowLogs.Size = new Size(76, 23);
            btnShowLogs.TabIndex = 17;
            btnShowLogs.Text = "Pokaż logi";
            btnShowLogs.UseVisualStyleBackColor = true;
            btnShowLogs.Click += BtnShowLogs_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 385);
            Controls.Add(btnShowLogs);
            Controls.Add(btnGenerateKeys);
            Controls.Add(lblKey);
            Controls.Add(txtKey);
            Controls.Add(btnDecryptText);
            Controls.Add(btnEncryptText);
            Controls.Add(cmbAlgorithms);
            Controls.Add(lblFileStatus);
            Controls.Add(btnDecryptFile);
            Controls.Add(btnEncryptFile);
            Controls.Add(lblCipherText);
            Controls.Add(lblPlainText);
            Controls.Add(txtCipherText);
            Controls.Add(txtPlainText);
            MinimumSize = new Size(571, 424);
            Name = "MainForm";
            Text = "Aplikacja Kryptograficzna";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox txtPlainText;
        private System.Windows.Forms.TextBox txtCipherText;
        private System.Windows.Forms.Label lblPlainText;
        private System.Windows.Forms.Label lblCipherText = null!;
        private System.Windows.Forms.Button btnEncryptFile;
        private System.Windows.Forms.Button btnDecryptFile;
        private System.Windows.Forms.Label lblFileStatus;
        private Label lblKey;
        private TextBox txtKey;
        private Button btnDecryptText;
        private Button btnEncryptText;
        private Label lblCurrentCipher;
        private ComboBox cmbAlgorithms;
        private Button btnGenerateKeys;
        private Button btnShowLogs;
    }
}