using System;
using Xunit;
using SS.Rijndael.Cryptography;
using SS.Rijndael.Cryptography.Exceptions;

namespace SS.Rijndael.Cryptography.UnitTests
{
    public class RijndealTest
    {
        [Theory]
        [InlineData("test")]
        [InlineData("mariusz pudzinakowski")]
        [InlineData("dsds da dad sadsad a dsad sadsadsadsadsadsadsadsadsadsa dsad sads ad")]
        public void EncryptStringToBytes_ShouldReturn_EncryptedValue(string value)
        {
            var rijandeal = new RijndealCryptography();
            var IV = rijandeal.GenerateIV();
            var KEY = rijandeal.GenerateKey();
            var encrypted = rijandeal.EncryptStringToBytes(value, KEY, IV);
            var decrypted = rijandeal.DecryptStringFromBytes(encrypted, KEY, IV);
            Assert.Equal(value, decrypted);
        }

        [Fact]
        public void EncryptStringToBytes_ShoulThrowExcpetion_When_KeyIsNull()
        {
            var rijandeal = new RijndealCryptography();
            var IV = rijandeal.GenerateIV();
            Assert.Throws<KeyException>(
                () =>
                    rijandeal.EncryptStringToBytes("test", null, IV)
            );
        }

        [Fact]
        public void EncryptStringToBytes_ShoulThrowExcpetion_When_IVIsNull()
        {
            var rijandeal = new RijndealCryptography();
            var KEY = rijandeal.GenerateKey();
            Assert.Throws<IVException>(
                () =>
                    rijandeal.EncryptStringToBytes("test", KEY, null)
            ) ;
        }
        [Fact]
        public void EncryptStringToBytes_ShoulThrowExcpetion_When_textIsnull()
        {
            var rijandeal = new RijndealCryptography();
            var KEY = rijandeal.GenerateKey();
            var IV = rijandeal.GenerateIV();
            Assert.Throws<RijndealException>(
                () =>
                    rijandeal.EncryptStringToBytes(null, KEY, IV)
            );
        }
        [Fact]
        public void DecryptStringFromBytes_ShoulThrowExcpetion_When_KeyIsNull()
        {
            var rijandeal = new RijndealCryptography();
            var IV = rijandeal.GenerateIV();
            Assert.Throws<KeyException>(
                () =>
                    rijandeal.DecryptStringFromBytes(new byte[] {0,1 }, null, IV)
            );
        }
        [Fact]
        public void DecryptStringFromBytes_ShoulThrowExcpetion_When_IVIsNull()
        {
            var rijandeal = new RijndealCryptography();
            var KEY = rijandeal.GenerateKey();
            Assert.Throws<IVException>(
                () =>
                    rijandeal.DecryptStringFromBytes(new byte[] { 0, 1 },KEY,null)
            );
        }

        [Fact]
        public void DecryptStringFromBytes_ShoulThrowExcpetion_When_textIsnull()
        {
            var rijandeal = new RijndealCryptography();
            var KEY = rijandeal.GenerateKey();
            var IV = rijandeal.GenerateIV();
            Assert.Throws<RijndealException>(
                () =>
                    rijandeal.DecryptStringFromBytes(null, KEY, IV)
            );
        }
    }
}
