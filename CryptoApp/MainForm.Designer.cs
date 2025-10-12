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
            this.txtPlainText = new System.Windows.Forms.TextBox();
            this.txtCipherText = new System.Windows.Forms.TextBox();
            this.btnEncryptText = new System.Windows.Forms.Button();
            this.btnDecryptText = new System.Windows.Forms.Button();
            this.lblPlainText = new System.Windows.Forms.Label();
            this.lblCipherText = new System.Windows.Forms.Label();
            this.lblKey = new System.Windows.Forms.Label();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.btnEncryptFile = new System.Windows.Forms.Button();
            this.btnDecryptFile = new System.Windows.Forms.Button();
            this.lblFileStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtPlainText
            // 
            this.txtPlainText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPlainText.Location = new System.Drawing.Point(12, 38);
            this.txtPlainText.Multiline = true;
            this.txtPlainText.Name = "txtPlainText";
            this.txtPlainText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtPlainText.Size = new System.Drawing.Size(776, 120);
            this.txtPlainText.TabIndex = 0;
            // 
            // txtCipherText
            // 
            this.txtCipherText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCipherText.Location = new System.Drawing.Point(12, 218);
            this.txtCipherText.Multiline = true;
            this.txtCipherText.Name = "txtCipherText";
            this.txtCipherText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCipherText.Size = new System.Drawing.Size(776, 120);
            this.txtCipherText.TabIndex = 1;
            // 
            // btnEncryptText
            // 
            this.btnEncryptText.Location = new System.Drawing.Point(232, 175);
            this.btnEncryptText.Name = "btnEncryptText";
            this.btnEncryptText.Size = new System.Drawing.Size(120, 29);
            this.btnEncryptText.TabIndex = 2;
            this.btnEncryptText.Text = "Szyfruj Tekst";
            this.btnEncryptText.UseVisualStyleBackColor = true;
            this.btnEncryptText.Click += new System.EventHandler(this.btnEncryptText_Click);
            // 
            // btnDecryptText
            // 
            this.btnDecryptText.Location = new System.Drawing.Point(358, 175);
            this.btnDecryptText.Name = "btnDecryptText";
            this.btnDecryptText.Size = new System.Drawing.Size(120, 29);
            this.btnDecryptText.TabIndex = 3;
            this.btnDecryptText.Text = "Deszyfruj Tekst";
            this.btnDecryptText.UseVisualStyleBackColor = true;
            this.btnDecryptText.Click += new System.EventHandler(this.btnDecryptText_Click);
            // 
            // lblPlainText
            // 
            this.lblPlainText.AutoSize = true;
            this.lblPlainText.Location = new System.Drawing.Point(12, 18);
            this.lblPlainText.Name = "lblPlainText";
            this.lblPlainText.Size = new System.Drawing.Size(89, 17);
            this.lblPlainText.TabIndex = 4;
            this.lblPlainText.Text = "Tekst Jawny:";
            // 
            // lblCipherText
            // 
            this.lblCipherText.AutoSize = true;
            this.lblCipherText.Location = new System.Drawing.Point(12, 198);
            this.lblCipherText.Name = "lblCipherText";
            this.lblCipherText.Size = new System.Drawing.Size(139, 17);
            this.lblCipherText.TabIndex = 5;
            this.lblCipherText.Text = "Tekst Zaszyfrowany:";
            // 
            // lblKey
            // 
            this.lblKey.AutoSize = true;
            this.lblKey.Location = new System.Drawing.Point(12, 182);
            this.lblKey.Name = "lblKey";
            this.lblKey.Size = new System.Drawing.Size(125, 17);
            this.lblKey.TabIndex = 7;
            this.lblKey.Text = "Klucz (Przesunięcie):";
            // 
            // txtKey
            // 
            this.txtKey.Location = new System.Drawing.Point(143, 179);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(83, 22);
            this.txtKey.TabIndex = 6;
            this.txtKey.Text = "3"; // Domyślny klucz dla Cezara
            // 
            // btnEncryptFile
            // 
            this.btnEncryptFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEncryptFile.Location = new System.Drawing.Point(12, 362);
            this.btnEncryptFile.Name = "btnEncryptFile";
            this.btnEncryptFile.Size = new System.Drawing.Size(120, 29);
            this.btnEncryptFile.TabIndex = 8;
            this.btnEncryptFile.Text = "Szyfruj Plik";
            this.btnEncryptFile.UseVisualStyleBackColor = true;
            this.btnEncryptFile.Click += new System.EventHandler(this.btnEncryptFile_Click);
            // 
            // btnDecryptFile
            // 
            this.btnDecryptFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDecryptFile.Location = new System.Drawing.Point(138, 362);
            this.btnDecryptFile.Name = "btnDecryptFile";
            this.btnDecryptFile.Size = new System.Drawing.Size(120, 29);
            this.btnDecryptFile.TabIndex = 9;
            this.btnDecryptFile.Text = "Deszyfruj Plik";
            this.btnDecryptFile.UseVisualStyleBackColor = true;
            this.btnDecryptFile.Click += new System.EventHandler(this.btnDecryptFile_Click);
            // 
            // lblFileStatus
            // 
            this.lblFileStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblFileStatus.AutoSize = true;
            this.lblFileStatus.Location = new System.Drawing.Point(270, 369);
            this.lblFileStatus.Name = "lblFileStatus";
            this.lblFileStatus.Size = new System.Drawing.Size(107, 17);
            this.lblFileStatus.TabIndex = 10;
            this.lblFileStatus.Text = "Status: Gotowy";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 410);
            this.Controls.Add(this.lblFileStatus);
            this.Controls.Add(this.btnDecryptFile);
            this.Controls.Add(this.btnEncryptFile);
            this.Controls.Add(this.lblKey);
            this.Controls.Add(this.txtKey);
            this.Controls.Add(this.lblCipherText);
            this.Controls.Add(this.lblPlainText);
            this.Controls.Add(this.btnDecryptText);
            this.Controls.Add(this.btnEncryptText);
            this.Controls.Add(this.txtCipherText);
            this.Controls.Add(this.txtPlainText);
            this.MinimumSize = new System.Drawing.Size(650, 450);
            this.Name = "MainForm";
            this.Text = "Aplikacja Kryptograficzna - Szyfr Cezara";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPlainText;
        private System.Windows.Forms.TextBox txtCipherText;
        private System.Windows.Forms.Button btnEncryptText;
        private System.Windows.Forms.Button btnDecryptText;
        private System.Windows.Forms.Label lblPlainText;
        private System.Windows.Forms.Label lblCipherText;
        private System.Windows.Forms.Label lblKey;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.Button btnEncryptFile;
        private System.Windows.Forms.Button btnDecryptFile;
        private System.Windows.Forms.Label lblFileStatus;
    }
}