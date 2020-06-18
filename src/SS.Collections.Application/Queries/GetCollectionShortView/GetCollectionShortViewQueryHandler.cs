using SS.Collections.Application.Configuration.Queries;
using SS.Collections.Application.ReadModels;
using SS.Infrastructure.PagginationList;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SS.Collections.Application.GetCollectionShortView
{
    internal class GetCollectionShortViewQueryHandler : IQueryHandler<GetCollectionShortViewQuery, PagedList<CollectionShortView>>
    {
        public readonly ICollectionQueryService _service;
        public GetCollectionShortViewQueryHandler(ICollectionQueryService service)
        {
            _service = service;
        }
        public async Task<PagedList<CollectionShortView>> Handle(GetCollectionShortViewQuery request, CancellationToken cancellationToken)
            => await _service.GetCollectionsbyOrganizationId(request.OrganizationId, request.PageNumber, request.PageSize);

    }
}
