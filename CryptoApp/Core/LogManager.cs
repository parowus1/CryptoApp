using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CryptoApp.Core
{
    public class LogEntry
    {
        public DateTime Timestamp { get; set; }
        public string Algorithm { get; set; }
        public string Operation { get; set; } 
        public string Status { get; set; }    
        public string Message { get; set; }
        public string KeyUsed { get; set; }

        public override string ToString()
        {
            return $"{Timestamp:HH:mm:ss.fff} | [{Algorithm} - {Operation}] {Status}: {Message}";
        }
    }

    public static class LogManager
    {
        private static readonly List<LogEntry> logs = new List<LogEntry>();

        public static IReadOnlyList<LogEntry> GetLogs()
        {
            return logs.AsReadOnly();
        }

        public static void ClearLogs()
        {
            logs.Clear();
        }

        public static void AddLog(string algorithm, string operation, string message, string status, string keyUsed)
        {
            var newEntry = new LogEntry
            {
                Timestamp = DateTime.Now,
                Algorithm = algorithm,
                Operation = operation,
                Status = status,
                Message = message,
                KeyUsed = (status == "SUCCESS") ? keyUsed : "N/A"
            };
            logs.Add(newEntry);
        }
    }
}