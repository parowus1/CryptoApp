using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.Core
{
    public class VigenereCipher : ICipher

    {

        public string Name => "Szyfr Vigenère'a";

        private const int AlphabetLength = 26;


        private string ProcessText(string text, string keyword, bool decrypt = false)

        {


            string cleanKeyword = new string(keyword.Where(char.IsLetter).ToArray()).ToUpper();

            if (string.IsNullOrEmpty(cleanKeyword))

            {

                throw new ArgumentException("Klucz Vigenère'a musi zawierać co najmniej jedną literę.");

            }

            var result = new StringBuilder();

            int keywordIndex = 0;

            foreach (char character in text)

            {

                if (char.IsLetter(character))

                {


                    char offset = char.IsUpper(character) ? 'A' : 'a';


                    char keyChar = cleanKeyword[keywordIndex % cleanKeyword.Length];

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


                    keywordIndex++;

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
