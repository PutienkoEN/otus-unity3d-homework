using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Zenject;

namespace Homeworks.SaveLoad.Scripts.Security
{
    public class AesEncryptor
    {
        private readonly string encryptionKey;

        [Inject]
        public AesEncryptor(string encryptionKey)
        {
            this.encryptionKey = encryptionKey;
        }

        public string Encrypt(string plainText)
        {
            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(encryptionKey);
            aes.GenerateIV();

            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using var memoryStream = new MemoryStream();

            memoryStream.Write(aes.IV, 0, aes.IV.Length);
            using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            using var streamWriter = new StreamWriter(cryptoStream);

            streamWriter.Write(plainText);

            return Convert.ToBase64String(memoryStream.ToArray());
        }

        public string Decrypt(string cipherText)
        {
            var fullCipher = Convert.FromBase64String(cipherText);
            var key = Encoding.UTF8.GetBytes(encryptionKey);

            using var aes = Aes.Create();
            aes.Key = key;

            var iv = new byte[aes.BlockSize / 8];
            var cipher = new byte[fullCipher.Length - iv.Length];

            aes.IV = iv;

            var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using var memoryStream = new MemoryStream(cipher);
            using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            using var streamReader = new StreamReader(cryptoStream);

            return streamReader.ReadToEnd();
        }
    }
}