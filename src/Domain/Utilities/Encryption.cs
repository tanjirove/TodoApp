using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Todo.Domain.Utilities
{
    public static class Encryption
    {
        private const string KEY = "FTBTW#DL91NWX10@KLV84ABCC$B7XRVK"; // Symmetric Key

        // AES Encryption
        public static string Encrypt(string text)
        {
            var keyBytes = Encoding.UTF8.GetBytes(KEY);

            try
            {
                using (var aes = Aes.Create())
                {
                    using (var encryptor = aes.CreateEncryptor(keyBytes, aes.IV))
                    {
                        using (var msEncrypt = new MemoryStream())
                        {
                            using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                            {
                                using (var swEncrypt = new StreamWriter(csEncrypt))
                                {
                                    swEncrypt.Write(text);
                                }
                            }

                            var iv = aes.IV;

                            var decryptedContent = msEncrypt.ToArray();

                            var result = new byte[iv.Length + decryptedContent.Length];

                            Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                            Buffer.BlockCopy(decryptedContent, 0, result, iv.Length, decryptedContent.Length);

                            return Convert.ToBase64String(result);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error in encryption method", e);
            }
        }

        // AES Decryption
        public static string Decrypt(string cipherText)
        {
            try
            {
                var keyBytes = Encoding.UTF8.GetBytes(KEY);

                var fullCipher = Convert.FromBase64String(cipherText);

                var iv = new byte[16];
                var cipher = new byte[fullCipher.Length - iv.Length];

                Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
                Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, fullCipher.Length - iv.Length);

                using (var aes = Aes.Create())
                {
                    using (var decryptor = aes.CreateDecryptor(keyBytes, iv))
                    {
                        string result;

                        using (var msDecrypt = new MemoryStream(cipher))
                        {
                            using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                            {
                                using (var srDecrypt = new StreamReader(csDecrypt))
                                {
                                    result = srDecrypt.ReadToEnd();
                                }
                            }
                        }

                        return result;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Invalid encrypted text", e);
            }
        }
    }
}
