using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CryptoApp.Core;
using static System.Reflection.Metadata.BlobBuilder;

namespace CryptoApp
{
    public partial class LogWindow : Form
    {
        public LogWindow()
        {
            InitializeComponent();
            this.Text = "Logi aplikacji i analiza";

            btnClearLogs.Click += BtnClearLogs_Click;
            btnAnalyze.Click += BtnAnalyze_Click;

            RefreshLogs();
        }

        public void RefreshLogs()
        {
            if (lbLogs.InvokeRequired)
            {
                lbLogs.BeginInvoke(new MethodInvoker(RefreshLogs));
                return;
            }

            lbLogs.Items.Clear();
            foreach (var log in LogManager.GetLogs().Reverse()) 
            {
                lbLogs.Items.Add(log.ToString());
            }
        }

        private void BtnClearLogs_Click(object sender, EventArgs e)
        {
            LogManager.ClearLogs();
            RefreshLogs();
        }

        private void InitializeComponent()
        {
            lbLogs = new ListBox();
            btnAnalyze = new Button();
            btnClearLogs = new Button();
            SuspendLayout();
            // 
            // lbLogs
            // 
            lbLogs.FormattingEnabled = true;
            lbLogs.ItemHeight = 15;
            lbLogs.Location = new Point(12, 21);
            lbLogs.Name = "lbLogs";
            lbLogs.Size = new Size(546, 154);
            lbLogs.TabIndex = 0;
            lbLogs.SelectedIndexChanged += lbLogs_SelectedIndexChanged;
            // 
            // btnAnalyze
            // 
            btnAnalyze.Location = new Point(12, 203);
            btnAnalyze.Name = "btnAnalyze";
            btnAnalyze.Size = new Size(71, 23);
            btnAnalyze.TabIndex = 1;
            btnAnalyze.Text = "Analiza";
            btnAnalyze.UseVisualStyleBackColor = true;
            // 
            // btnClearLogs
            // 
            btnClearLogs.Location = new Point(89, 203);
            btnClearLogs.Name = "btnClearLogs";
            btnClearLogs.Size = new Size(105, 23);
            btnClearLogs.TabIndex = 2;
            btnClearLogs.Text = "Wyczyść logi";
            btnClearLogs.UseVisualStyleBackColor = true;
            // 
            // LogWindow
            // 
            ClientSize = new Size(570, 261);
            Controls.Add(btnClearLogs);
            Controls.Add(btnAnalyze);
            Controls.Add(lbLogs);
            Name = "LogWindow";
            ResumeLayout(false);
        }

        private void BtnAnalyze_Click(object sender, EventArgs e)
        {
            var logs = LogManager.GetLogs();

            if (!logs.Any())
            {
                MessageBox.Show("Brak logów do analizy.", "Analiza", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var lastLog = logs.Last();

            StringBuilder report = new StringBuilder();
            report.AppendLine("--- Szczegółowa analiza sstatniej operacji ---");
            report.AppendLine($"Czas: {lastLog.Timestamp:yyyy-MM-dd HH:mm:ss.fff}");
            report.AppendLine($"Algorytm: {lastLog.Algorithm}");
            report.AppendLine($"Operacja: {lastLog.Operation}");
            report.AppendLine($"Status: {lastLog.Status}");
            report.AppendLine("----------------------------------------------");
            report.AppendLine($"Klucz użyty: {lastLog.KeyUsed}");
            report.AppendLine($"Pełna Wiadomość: {lastLog.Message}");

            MessageBox.Show(report.ToString(), $"Analiza: {lastLog.Operation} - {lastLog.Algorithm}", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void lbLogs_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private ListBox lbLogs;
        private Button btnClearLogs;
        private Button btnAnalyze;
    }
}