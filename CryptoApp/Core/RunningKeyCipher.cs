using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.Core
{
    public class RunningKeyCipher : ICipher
    {
        public string Name => "Szyfr z Kluczem Bieżącym";

        private const int AlphabetLength = 26;

        private string ProcessText(string text, string runningKey, bool decrypt = false)
        {
            string cleanText = new string(text.Where(char.IsLetter).ToArray()).ToUpper();

            string cleanKey = new string(runningKey.Where(char.IsLetter).ToArray()).ToUpper();

            if (string.IsNullOrEmpty(cleanKey))
            {
                throw new ArgumentException("Klucz bieżący musi zawierać litery.");
            }

            if (cleanKey.Length < cleanText.Length)
            {
                throw new ArgumentException($"Klucz bieżący musi być co najmniej tak długi ({cleanText.Length} liter) jak czysty tekst, który ma zostać zaszyfrowany.");
            }

            var result = new StringBuilder();
            int cleanTextIndex = 0; 

            foreach (char character in text)
            {
                if (char.IsLetter(character))
                {
                    char offset = char.IsUpper(character) ? 'A' : 'a';

                    char keyChar = cleanKey[cleanTextIndex];
                    int shift = keyChar - 'A';

                    int charPosition = character - offset;

                    int newPosition;

                    if (decrypt)
                    {
                        newPosition = (charPosition - shift + AlphabetLength) % AlphabetLength;
                    }
                    else
                    {
  
                        newPosition = (charPosition + shift) % AlphabetLength;
                    }

                    char newChar = (char)(newPosition + offset);
                    result.Append(newChar);

                    cleanTextIndex++;
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
            return ProcessText(plainText, key, decrypt: false);
        }

        public string DecryptText(string cipherText, string key)
        {
            return ProcessText(cipherText, key, decrypt: true);
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