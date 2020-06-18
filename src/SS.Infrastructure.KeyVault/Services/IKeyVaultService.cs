using SS.Infrastructure.KeyVault.Model;
using System;
using System.Threading.Tasks;

namespace SS.Infrastructure.KeyVault.Services
{
    public interface IKeyVaultService
    {
        Task<StorageKey> GetKeyByResourceId(Guid resourceId);
        Task AddKeyAsync(StorageKey key);
    }
}
