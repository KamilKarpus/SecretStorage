﻿using SS.Rijndael.Cryptography.Exceptions;
using System.IO;
using System.Security.Cryptography;

namespace SS.Rijndael.Cryptography
{
    public class RijndealCryptography : IRijndealCryptography
    {
        private readonly RijndaelManaged _rijndaelManaged;
        public RijndealCryptography()
        {
            _rijndaelManaged = new RijndaelManaged();
        }
        public byte[] GenerateIV()
        {
            _rijndaelManaged.GenerateIV();
            return _rijndaelManaged.IV;
        }
        public byte[] GenerateKey()
        {
            _rijndaelManaged.GenerateKey();
            return _rijndaelManaged.Key;
        }
        public byte[] EncryptStringToBytes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new RijndealException(HttpCodes.InternalServerError,ExceptionCode.PlainTextIsNull,"Plain Text is null");
            if (Key == null || Key.Length <= 0)
                throw new KeyException(HttpCodes.InternalServerError, ExceptionCode.KeyNullValue, "The key value is null");
            if (IV == null || IV.Length <= 0)
                throw new IVException(HttpCodes.InternalServerError, ExceptionCode.KeyNullValue, "The IV value is null");
            byte[] encrypted;
            // Create an RijndaelManaged object
            // with the specified key and IV.
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for encryption.
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            return encrypted;
        }
        public string DecryptStringFromBytes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new RijndealException(HttpCodes.InternalServerError,ExceptionCode.EncryptedValueIsNull, "Encrypted value is null");
            if (Key == null || Key.Length <= 0)
                throw new KeyException(HttpCodes.InternalServerError,ExceptionCode.KeyNullValue, "The key value is null");
            if (IV == null || IV.Length <= 0)
                throw new IVException(HttpCodes.InternalServerError,ExceptionCode.KeyNullValue, "The IV value is null");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an RijndaelManaged object
            // with the specified key and IV.
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }
    }
}
