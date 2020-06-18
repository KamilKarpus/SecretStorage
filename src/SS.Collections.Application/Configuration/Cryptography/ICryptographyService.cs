using System;
using System.Threading.Tasks;

namespace SS.Collections.Application.Cryptography
{
    public interface ICryptographyService
    {
        Task<byte[]> Encrypt(Guid objectId, string value);
        Task<string> Decrypt(Guid objectId, byte[] source);
        Task<byte[]> UpdateValue(Guid objectId, string value);
    }
}
