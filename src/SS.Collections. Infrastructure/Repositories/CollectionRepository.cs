using SS.Collections.Domain;
using SS.Collections.Domain.Repositories;
using SS.Collections.Infrastructure.Documents;
using SS.Collections.Infrastructure.Documents.Resources;
using SS.Common.EventDispatchers;
using SS.Common.Mongo;
using System;
using System.Threading.Tasks;

namespace SS.Collections._Infrastructure.Repositories
{
    public class CollectionRepository : ICollectionRepository
    {
        private readonly IMongoRepository<CollectionDocument, Guid> _repository;
        private readonly IEventDispatcher _dispatcher;
        public CollectionRepository(IMongoRepository<CollectionDocument, Guid> repository,
           IEventDispatcher dispatcher)
        {
            _repository = repository;
            _dispatcher = dispatcher;
        }

        public async Task AddAsync(Collection collection)
        {
            await _repository.AddAsync(collection.ToDocument());
            await _dispatcher.Dispatch(collection);
        }

        public async Task Delete(Collection collection)
        {
            await _repository.Delete(collection.ToDocument());
            await _dispatcher.Dispatch(collection);
        }

        public async Task<Collection> GetbyId(Guid id)
            => (await _repository.GetAsync(p => p.Id == id))?.AsEntity();

        public async Task<Collection> GetbyName(string name)
            => (await _repository.GetAsync(p => p.Name == name))?.AsEntity();

        public async Task<Collection> GetbyOrganizationId(Guid id)
            => (await _repository.GetAsync(p => p.OrganizationId == id))?.AsEntity();
           
        public async Task UpdateAsync(Collection collection)
        {
            await _repository.Update(collection.ToDocument());
            await _dispatcher.Dispatch(collection);
        }
    }
}
