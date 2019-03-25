using System;
using System.IO;
using System.Security.Cryptography;

namespace OutReachBusinessLayer
{
    public class AESCrypt
    {
        public static string EncryptString(string plainText)
        {
            string cipherText = "";
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.BlockSize = 128;
                aesAlg.KeySize = 128;
                aesAlg.Mode = CipherMode.CBC;
                //aesAlg.Padding = PaddingMode.Zeros;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(ConstantValues.VectorBytes, ConstantValues.KeyBytes);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        byte[] encrypted = msEncrypt.ToArray();
                        cipherText = Convert.ToBase64String(encrypted);
                    }
                }
            }
            return cipherText;
        }
        public static string DecryptString(string cipherText)
        {
            string plaintext = "";
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.BlockSize = 128;
                aesAlg.KeySize = 128;
                aesAlg.Mode = CipherMode.CBC;
                //aesAlg.Padding = PaddingMode.Zeros;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(ConstantValues.VectorBytes, ConstantValues.KeyBytes);
                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            return plaintext;
        }
    }
}