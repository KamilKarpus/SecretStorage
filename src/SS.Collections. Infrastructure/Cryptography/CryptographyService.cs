using SS.Collections.Application.Cryptography;
using SS.Infrastructure.KeyVault;
using SS.Rijndael.Cryptography;
using System;
using System.Threading.Tasks;

namespace SS.Collections._Infrastructure.Cryptography
{
    public class CryptographyService : ICryptographyService
    {
        private readonly IRijndealCryptography _cryptography;
        private readonly IKeyVault _keyVault;

        public CryptographyService(IRijndealCryptography cryptography, IKeyVault keyVault)
        {
            _keyVault = keyVault;
            _cryptography = cryptography;
        }
        public async Task<byte[]> Encrypt(Guid objectId, string value)
        {
            var key = _cryptography.GenerateKey();
            var iv = _cryptography.GenerateIV();
            var cryptedValue = _cryptography.EncryptStringToBytes(value, key, iv);
            await _keyVault.AddKey(objectId, key, iv);
            return cryptedValue;
        }

        public async Task<string> Decrypt(Guid objectId, byte[] source)
        {
            var keys = await _keyVault.GetKeybyId(objectId);
            var encryptedValue = _cryptography.DecryptStringFromBytes(source, keys.Key, keys.IV);
            return encryptedValue;
        }

        public async Task<byte[]> UpdateValue(Guid objectId, string value)
        {
            var keys = await _keyVault.GetKeybyId(objectId);
            var cryptedValue = _cryptography.EncryptStringToBytes(value, keys.Key, keys.IV);
            return cryptedValue;
        }
    }
}
