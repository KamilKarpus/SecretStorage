using SS.Collections.Application.Configuration.Queries;
using SS.Collections.Application.ReadModels;
using SS.Collections.Infrastructure.Documents.Resources;
using SS.Common.Mongo;
using SS.Infrastructure.PagginationList;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SS.Collections._Infrastructure.QueriesServices
{
    public class CollectionQueryService : ICollectionQueryService
    {
        private readonly IMongoQueryClient<CollectionDocument, Guid> _client;

        public CollectionQueryService(IMongoQueryClient<CollectionDocument, Guid> client)
        {
            _client = client;
        }

        public async Task<PagedList<CollectionShortView>> GetCollectionsbyOrganizationId(Guid id, int number, int pageSize)
            => (await _client.QueryMany(p => p.OrganizationId == id))
                .Select(p => new CollectionShortView()
                {
                    Id = p.Id,
                    Name = p.Name
                }).ToPagedList(number,pageSize);

        public async Task<CollectionInfoView> GetCollectionbyId(Guid id)
        {
            var result = await _client.Query(p => p.Id == id);
            return new CollectionInfoView()
            {
                Id = result.Id,
                Name = result.Name,
                Resources = result.Resources.Select(p => new ResourceShortView()
                {
                    Id = p.Id,
                    EditedTime = p.EditedTime,
                    Name = p.Name,
                    ReadedTime = p.ReadedTime
                }).ToList()
            };
        }

        public async Task<ResourceInfoView> GetResourcebyId(Guid collectionId, Guid resourceId)
        {
            var collection = await _client.Query(p => p.Id == collectionId);
            var result = collection.Resources.FirstOrDefault(p => p.Id == resourceId);
            return new ResourceInfoView()
            {
                Id = result.Id,
                Owner = result.Owner.DisplayName,
                EditedBy = result.EditedBy.DisplayName,
                ReadedBy = result.ReadedBy.DisplayName,
                EditedTime = result.EditedTime,
                ReadedTime = result.ReadedTime,
                Name = result.Name
            };
        }
    }
}
