using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.Core
{
    public interface ICipher
    {
        string Name { get; }

        string EncryptText(string plainText, string key);

        string DecryptText(string cipherText, string key);
        Task EncryptFileAsync(string inputFilePath, string outputFilePath, string key);

        Task DecryptFileAsync(string inputFilePath, string outputFilePath, string key);
    }
}
