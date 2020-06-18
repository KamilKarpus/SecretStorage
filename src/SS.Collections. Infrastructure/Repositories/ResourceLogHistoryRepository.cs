using SS.Collections._Infrastructure.Documents.History;
using SS.Collections.Domain.History;
using SS.Collections.Domain.Repositories;
using SS.Collections.Infrastructure.Documents;
using SS.Common.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SS.Collections._Infrastructure.Repositories
{
    public class ResourceLogHistoryRepository : IResourceLogHistoryRepository
    {
        private readonly IMongoRepository<ResourceLogHistoryDocument, Guid> _repository;
        public ResourceLogHistoryRepository(IMongoRepository<ResourceLogHistoryDocument, Guid> repository)
        {
            _repository = repository;
        }
        public async Task AddAsync(ResourceLogHistory history)
            => await _repository.AddAsync(history.ToDocument());

        public async Task<ResourceLogHistory> GetbyResourceId(Guid resourceId)
           => (await _repository.GetAsync(p => p.ResourceId == resourceId))?.AsEntity();

        public async Task<List<ResourceLogHistory>> GetManybyCollectionId(Guid id)
        {
           var result = await _repository.FindAsync(p => p.CollectionId == id);
           return result.Select(p => p?.AsEntity())
                 ?.ToList();
        }

        public async Task Remove(ResourceLogHistory resourceLogHistory)
            => await _repository.Delete(resourceLogHistory.ToDocument());
         
        

        public async Task RemoveManybyCollectionId(Guid collectionId)
            => await _repository.DeleteMany(p => p.CollectionId == collectionId);

        public async Task UpdateAsync(ResourceLogHistory history)
           => await _repository.Update(history.ToDocument());
        
    }
}
