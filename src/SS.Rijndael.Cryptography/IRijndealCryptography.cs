namespace SS.Rijndael.Cryptography
{
    public interface IRijndealCryptography
    {
        byte[] GenerateIV();
        byte[] GenerateKey();
        byte[] EncryptStringToBytes(string plainText, byte[] Key, byte[] IV);
        string DecryptStringFromBytes(byte[] cipherText, byte[] Key, byte[] IV);

    }
}
