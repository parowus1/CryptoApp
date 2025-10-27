using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.Core
{
    public class AESCipher : ICipher
    {
        public string Name => "AES-256 (GCM)";

        // --- Sta³e i Konfiguracja AES ---
        private const int AesKeySize = 32;
        private const int AesIVSize = 12;

        // --- Implementacja interfejsu ICipher (Tekst) ---

        private static byte[] GenerateKeyFromPassword(string key)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] keyBytes = Encoding.UTF8.GetBytes(key);
                byte[] hash = sha256.ComputeHash(keyBytes);

                return hash.Take(AesKeySize).ToArray();
            }
        }

        public string EncryptText(string plainText, string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Klucz AES nie mo¿e byæ pusty.");
            }

            byte[] keyBytes = GenerateKeyFromPassword(key);
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

            using (var aesGcm = new AesGcm(keyBytes))
            {
                byte[] nonce = new byte[AesIVSize];
                RandomNumberGenerator.Fill(nonce);

                byte[] tag = new byte[AesGcm.TagByteSizes.MaxSize];

                byte[] cipherBytes = new byte[plainBytes.Length];

                aesGcm.Encrypt(nonce, plainBytes, cipherBytes, tag);

                byte[] finalBytes = new byte[nonce.Length + tag.Length + cipherBytes.Length];
                Buffer.BlockCopy(nonce, 0, finalBytes, 0, nonce.Length);
                Buffer.BlockCopy(tag, 0, finalBytes, nonce.Length, tag.Length);
                Buffer.BlockCopy(cipherBytes, 0, finalBytes, nonce.Length + tag.Length, cipherBytes.Length);

                return Convert.ToBase64String(finalBytes);
            }
        }

        public string DecryptText(string cipherText, string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Klucz AES nie mo¿e byæ pusty.");
            }

            byte[] keyBytes = GenerateKeyFromPassword(key);
            byte[] cipherBytesWithNonceAndTag = Convert.FromBase64String(cipherText);

            if (cipherBytesWithNonceAndTag.Length < AesIVSize + AesGcm.TagByteSizes.MaxSize)
            {
                throw new CryptographicException("Niepoprawny format zaszyfrowanych danych.");
            }

            byte[] nonce = new byte[AesIVSize];
            byte[] tag = new byte[AesGcm.TagByteSizes.MaxSize];
            int cipherLength = cipherBytesWithNonceAndTag.Length - nonce.Length - tag.Length;
            byte[] cipherBytes = new byte[cipherLength];

            Buffer.BlockCopy(cipherBytesWithNonceAndTag, 0, nonce, 0, nonce.Length);
            Buffer.BlockCopy(cipherBytesWithNonceAndTag, nonce.Length, tag, 0, tag.Length);
            Buffer.BlockCopy(cipherBytesWithNonceAndTag, nonce.Length + tag.Length, cipherBytes, 0, cipherLength);

            using (var aesGcm = new AesGcm(keyBytes))
            {
                byte[] plainBytes = new byte[cipherLength];

                aesGcm.Decrypt(nonce, cipherBytes, tag, plainBytes);

                return Encoding.UTF8.GetString(plainBytes);
            }
        }

        // --- Implementacja interfejsu ICipher
        public async Task EncryptFileAsync(string inputFilePath, string outputFilePath, string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Klucz AES nie mo¿e byæ pusty.");
            }

            byte[] keyBytes = GenerateKeyFromPassword(key);

            byte[] iv = new byte[16]; 
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(iv);
            }

            using (Aes aes = Aes.Create())
            {
                aes.KeySize = 256;
                aes.Mode = CipherMode.CBC; 
                aes.Key = keyBytes;
                aes.IV = iv;

                using (var inputFileStream = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read))
                using (var outputFileStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
                {
                    await outputFileStream.WriteAsync(iv, 0, iv.Length);

                    using (var cryptoStream = new CryptoStream(outputFileStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        await inputFileStream.CopyToAsync(cryptoStream);
                    }
                }
            }
        }

        public async Task DecryptFileAsync(string inputFilePath, string outputFilePath, string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Klucz AES nie mo¿e byæ pusty.");
            }

            byte[] keyBytes = GenerateKeyFromPassword(key);
            byte[] iv = new byte[16]; 

            using (Aes aes = Aes.Create())
            {
                aes.KeySize = 256;
                aes.Mode = CipherMode.CBC;
                aes.Key = keyBytes;

                using (var inputFileStream = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read))
                {
                    int bytesRead = await inputFileStream.ReadAsync(iv, 0, iv.Length);
                    if (bytesRead != iv.Length)
                    {
                        throw new CryptographicException("Plik jest uszkodzony lub nie zawiera poprawnego Wektora Inicjuj¹cego.");
                    }
                    aes.IV = iv; 

                    using (var outputFileStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
                    {
                        using (var cryptoStream = new CryptoStream(inputFileStream, aes.CreateDecryptor(), CryptoStreamMode.Read))
                        {
                            await cryptoStream.CopyToAsync(outputFileStream);
                        }
                    }
                }
            }
        }
    }
}