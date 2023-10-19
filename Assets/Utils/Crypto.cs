using System;
using System.Text;
using System.Security.Cryptography;

public static class Crypto {

    private static string key = "H2h2xZe83AX90788QNqJXRiWX88xWI2b";
    private static ICryptoTransform Encryptor;
    private static ICryptoTransform Decryptor;

    static Crypto() {
        var rDel = new RijndaelManaged();
        rDel.Key = Encoding.UTF8.GetBytes(key);
        rDel.Mode = CipherMode.ECB;
        rDel.Padding = PaddingMode.PKCS7;
        Encryptor = rDel.CreateEncryptor();
        Decryptor = rDel.CreateDecryptor();
    }

    public static string Encrypt(string source) {
        if (string.IsNullOrEmpty(source)) {
            return string.Empty;
        }
        else {
            var bytes = Encoding.UTF8.GetBytes(source);
            var result = Encryptor.TransformFinalBlock(bytes, 0, bytes.Length);
            return Convert.ToBase64String(result, 0, result.Length);
        }
    }

    public static string Decrypt(string source) {
        if (string.IsNullOrEmpty(source)) {
            return string.Empty;
        }
        else {
            var bytes = Convert.FromBase64String(source);
            var result = Decryptor.TransformFinalBlock(bytes, 0, bytes.Length);
            return Encoding.UTF8.GetString(result);
        }
    }

}