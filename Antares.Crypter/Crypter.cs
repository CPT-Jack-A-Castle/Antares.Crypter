/*
 * Antares.Crypter
 * 
 * Author: Fikret Musk
 * GitHub: wgex
 * NuGet: fikret
 * 
 * All rights reserved. 2021
*/
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Antares.Crypter
{
    public class Crypter
    {
        public static string Mode = "MD5";

        public Crypter()
        {

        }

        public string Encrypt(string plaintext, string key)
        {
            string sifrelenmis = "Encrypting Failed";

            if (Mode == "AES")
            {
                sifrelenmis = AesSifrele(plaintext, key);
            }

            if (Mode == "RSA")
            {
                sifrelenmis = RsaSifrele(plaintext, key);
            }

            return sifrelenmis;
        }

        public string Decrypt(string encryptedtext, string key)
        {
            string desifrelenmis = "Decrypting Failed";

            if (Mode == "AES")
            {
                desifrelenmis = AesDesifrele(encryptedtext, key);
            }

            if (Mode == "RSA")
            {
                desifrelenmis = RsaDesifrele(encryptedtext, key);
            }

            return desifrelenmis;
        }

        public string Hash(string plaintext)
        {
            string hashlenmis = "Hashing Failed";

            if (Mode == "SHA256")
            {
                hashlenmis = ShaSifrele(plaintext);
            }

            if (Mode == "MD5")
            {
                hashlenmis = Md5Sifrele(plaintext);
            }

            return hashlenmis;
        }

        private string AesSifrele(string metin, string key)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(metin);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }

        private string AesDesifrele(string sifrelenmis, string key)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(sifrelenmis);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }

        private string RsaSifrele(string metin, string publickey)
        {
            var testData = Encoding.UTF8.GetBytes(metin);
            using (var rsa = new RSACryptoServiceProvider(1024))
            {
                try
                {
                    rsa.FromXmlString(publickey.ToString());
                    var encryptedData = rsa.Encrypt(testData, true);
                    var base64Encrypted = Convert.ToBase64String(encryptedData);
                    return base64Encrypted;
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }

        private string RsaDesifrele(string metin, string privatekey)
        {
            string fail = "Decrypting Failed";
            try
            {
                var testData = Encoding.UTF8.GetBytes(metin);
                using (var rsa = new RSACryptoServiceProvider(1024))
                {
                    try
                    {
                        var base64Encrypted = metin;
                        rsa.FromXmlString(privatekey);
                        var resultBytes = Convert.FromBase64String(base64Encrypted);
                        var decryptedBytes = rsa.Decrypt(resultBytes, true);
                        var decryptedData = Encoding.UTF8.GetString(decryptedBytes);
                        return decryptedData.ToString();
                    }
                    finally
                    {
                        rsa.PersistKeyInCsp = false;
                    }
                }
            }
            catch
            {
                return fail;
            }
        }

        private string ShaSifrele(string metin)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(metin));

                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        private string Md5Sifrele(string metin)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(metin);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }

                return sb.ToString();
            }
        }
    }

    public class CrypterMode
    {
        public static readonly string AES = "AES";
        public static readonly string RSA = "RSA";
    }

    public class HashMode
    {
        public static readonly string MD5 = "MD5";
        public static readonly string SHA256 = "SHA256";
    }
}
