using SS.Common.Mongo;
using SS.Infrastructure.KeyVault.Model;
using System;
using System.Threading.Tasks;

namespace SS.Infrastructure.KeyVault.Services
{
    public class KeyVaultService : IKeyVaultService
    {
        private readonly IMongoRepository<StorageKey, Guid> _repository;
        public KeyVaultService(IMongoRepository<StorageKey, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<StorageKey> GetKeyByResourceId(Guid resourceId)
            => await _repository.GetAsync(p=>p.ResourceId == resourceId);

        public async Task AddKeyAsync(StorageKey key)
            => await _repository.AddAsync(key);
        
    }
}
