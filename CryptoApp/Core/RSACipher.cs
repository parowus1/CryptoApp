using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; 

namespace CryptoApp.Core
{
    public class RSACipher : ICipher
    {
        public string Name => "RSA-2048";

        private const int KeySize = 2048;

        public static (string publicKey, string privateKey) GenerateKeys()
        {
            using (var rsa = RSA.Create())
            {
                rsa.KeySize = KeySize;
                string publicKey = rsa.ToXmlString(false);
                string privateKey = rsa.ToXmlString(true);

                return (publicKey, privateKey);
            }
        }

        public string EncryptText(string plainText, string publicKey)
        {
            if (string.IsNullOrEmpty(publicKey))
            {
                throw new ArgumentException("Wymagany jest Klucz Publiczny do szyfrowania.");
            }
            if (plainText.Length > (KeySize / 8) - 42) 
            {
                throw new ArgumentException($"Tekst jest za długi dla bezpośredniego szyfrowania RSA {KeySize} bitów. Limit znaków to około {(KeySize / 8) - 42}.");
            }

            try
            {
                using (var rsa = RSA.Create())
                {
                    rsa.FromXmlString(publicKey);

                    byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

                    byte[] cipherBytes = rsa.Encrypt(plainBytes, RSAEncryptionPadding.OaepSHA256);

                    return Convert.ToBase64String(cipherBytes);
                }
            }
            catch (Exception ex)
            {
                throw new CryptographicException($"Błąd szyfrowania RSA. Upewnij się, że klucz publiczny jest poprawny. Szczegóły: {ex.Message}");
            }
        }

        public string DecryptText(string cipherText, string privateKey)
        {
            if (string.IsNullOrEmpty(privateKey))
            {
                throw new ArgumentException("Wymagany jest Klucz Prywatny do deszyfrowania.");
            }

            try
            {
                using (var rsa = RSA.Create())
                {
                    rsa.FromXmlString(privateKey);

                    byte[] cipherBytes = Convert.FromBase64String(cipherText);

                    byte[] plainBytes = rsa.Decrypt(cipherBytes, RSAEncryptionPadding.OaepSHA256);

                    return Encoding.UTF8.GetString(plainBytes);
                }
            }
            catch (Exception ex)
            {
                throw new CryptographicException($"Błąd deszyfrowania RSA. Upewnij się, że klucz prywatny jest poprawny i dane nie zostały naruszone. Szczegóły: {ex.Message}");
            }
        }

        public async Task EncryptFileAsync(string inputFilePath, string outputFilePath, string key)
        {
            byte[] fileBytes = await File.ReadAllBytesAsync(inputFilePath);

            if (fileBytes.Length > (KeySize / 8) - 42)
            {
                throw new ArgumentException($"Plik jest za duży ({fileBytes.Length} bajtów) dla szyfrowania RSA. Użyj szyfru symetrycznego (np. AES).");
            }

            string base64Data = Convert.ToBase64String(fileBytes);
            string encryptedBase64 = EncryptText(base64Data, key);

            await File.WriteAllTextAsync(outputFilePath, encryptedBase64);
        }

        public async Task DecryptFileAsync(string inputFilePath, string outputFilePath, string key)
        {
            string encryptedBase64 = await File.ReadAllTextAsync(inputFilePath);

            string decryptedBase64 = DecryptText(encryptedBase64, key);

            byte[] decryptedBytes = Convert.FromBase64String(decryptedBase64);
            await File.WriteAllBytesAsync(outputFilePath, decryptedBytes);
        }
    }
}