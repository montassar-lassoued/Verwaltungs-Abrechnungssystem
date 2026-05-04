using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace MyControls
{
    public class PasswordManager
    {
        private static string KEY = @"!\°$2a$13$5zRtxO9g.6AI/bir/*55jrfv.&%";
        private static string IV = @"1B24C3C6AC68397C8C58076C77C85DC6CB47";

        public static string EncryptString(string plaintext)
        {
            // Convert the plaintext string to a byte array
            byte[] plaintextBytes = System.Text.Encoding.UTF8.GetBytes(plaintext);

            // Derive a new password using the PBKDF2 algorithm and a random salt
            Rfc2898DeriveBytes passwordBytes = new Rfc2898DeriveBytes(KEY, 1000);

            // Use the password to encrypt the plaintext
            Aes encryptor = Aes.Create();
            encryptor.Key = Encoding.UTF8.GetBytes(KEY).Take(16).ToArray();
            encryptor.IV = Encoding.UTF8.GetBytes(IV).Take(16).ToArray();

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(plaintextBytes, 0, plaintextBytes.Length);
                }
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        public static string DecryptString(string encrypted)
        {
            // Convert the encrypted string to a byte array
            byte[] encryptedBytes = Convert.FromBase64String(encrypted);

            // Derive the password using the PBKDF2 algorithm
            Rfc2898DeriveBytes passwordBytes = new Rfc2898DeriveBytes(KEY, 1000);

            // Use the password to decrypt the encrypted string
            Aes decryptor = Aes.Create();
            decryptor.Key = Encoding.UTF8.GetBytes(KEY).Take(16).ToArray();
            decryptor.IV = Encoding.UTF8.GetBytes(IV).Take(16).ToArray();
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, decryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(encryptedBytes, 0, encryptedBytes.Length);
                }
                return System.Text.Encoding.Default.GetString(ms.ToArray());
            }
        }
    }
}