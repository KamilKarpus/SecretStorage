using SS.Collections.Application.ReadModels;
using SS.Infrastructure.PagginationList;
using System;
using System.Threading.Tasks;

namespace SS.Collections.Application.Configuration.Queries
{
    public interface ICollectionQueryService
    {
        Task<PagedList<CollectionShortView>> GetCollectionsbyOrganizationId(Guid id, int number, int pageSize);
        Task<CollectionInfoView> GetCollectionbyId(Guid id);

        Task<ResourceInfoView> GetResourcebyId(Guid collectionId, Guid resourceId);
    }
}
