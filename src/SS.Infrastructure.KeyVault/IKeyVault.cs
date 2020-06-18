using SS.Infrastructure.KeyVault.ReadModels;
using System;
using System.Threading.Tasks;

namespace SS.Infrastructure.KeyVault
{
    public interface IKeyVault
    {
        Task<KeyView> GetKeybyId(Guid Id);
        Task AddKey(Guid resourceId, byte[] key, byte[] iv);
     }
}
