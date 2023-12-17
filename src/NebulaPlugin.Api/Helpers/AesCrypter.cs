using System.Security.Cryptography;
using System.Text;

namespace NebulaPlugin.Api.Helpers;

public static class AesCrypter
{
    private static byte[] aesKey = Convert.FromBase64String("GjU7DdxNUe5VaySL9Nl9Oq8EG8ZjJzm1+XthMj0ML/g=");

    public static string EncryptToString(string plainText)
    {
        byte[] encrypted;

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = aesKey;
            aesAlg.GenerateIV();

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length);

                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                }
                encrypted = msEncrypt.ToArray();
            }
        }

        return Convert.ToBase64String(encrypted);
    }

    public static string DecryptBytesToString(string encrypted)
    {
        byte[] cipherText = Convert.FromBase64String(encrypted);

        Console.WriteLine("BYTE ARRAY TESXT CHIPGER" + cipherText);
        string plaintext = null;

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = aesKey;

            byte[] iv = new byte[aesAlg.BlockSize / 8];
            Array.Copy(cipherText, 0, iv, 0, iv.Length);
            aesAlg.IV = iv;

            byte[] encryptedData = new byte[cipherText.Length - iv.Length];
            Array.Copy(cipherText, iv.Length, encryptedData, 0, encryptedData.Length);

            using (MemoryStream msDecrypt = new MemoryStream(encryptedData))
            {
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

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
