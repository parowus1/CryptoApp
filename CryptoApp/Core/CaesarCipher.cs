using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.Core
{
    public class CaesarCipher : ICipher
    {
        public string Name => "Szyfr Cezara";

        private string ProcessText(string text, int shift, bool decrypt = false)
        {
            if (decrypt)
            {
                shift = (26 - (shift % 26)) % 26;
            }

            var result = new StringBuilder();
            foreach (char character in text)
            {
                if (char.IsLetter(character))
                {
                    char offset = char.IsUpper(character) ? 'A' : 'a';
                    int charPosition = character - offset;

                    char newChar = (char)(((charPosition + shift) % 26) + offset);
                    result.Append(newChar);
                }
                else
                {
                    result.Append(character);
                }
            }
            return result.ToString();
        }

        public string EncryptText(string plainText, string key)
        {
            if (!int.TryParse(key, out int shift))
            {
                throw new ArgumentException("Klucz musi być liczbą całkowitą (przesunięciem).");
            }
            return ProcessText(plainText, shift);
        }

        public string DecryptText(string cipherText, string key)
        {
            if (!int.TryParse(key, out int shift))
            {
                throw new ArgumentException("Klucz musi być liczbą całkowitą (przesunięciem).");
            }
            return ProcessText(cipherText, shift, decrypt: true);
        }

        public async Task EncryptFileAsync(string inputFilePath, string outputFilePath, string key)
        {
            string fileContent = await File.ReadAllTextAsync(inputFilePath);
            string encryptedContent = EncryptText(fileContent, key);
            await File.WriteAllTextAsync(outputFilePath, encryptedContent);
        }

        public async Task DecryptFileAsync(string inputFilePath, string outputFilePath, string key)
        {
            string fileContent = await File.ReadAllTextAsync(inputFilePath);
            string decryptedContent = DecryptText(fileContent, key);
            await File.WriteAllTextAsync(outputFilePath, decryptedContent);
        }
    }
}
