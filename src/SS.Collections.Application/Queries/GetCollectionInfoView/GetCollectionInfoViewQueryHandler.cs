using SS.Collections.Application.Configuration.Queries;
using SS.Collections.Application.ReadModels;
using System.Threading;
using System.Threading.Tasks;

namespace SS.Collections.Application.GetCollectionInfoView
{
    public class GetCollectionInfoViewQueryHandler : IQueryHandler<GetCollectionInfoViewQuery, CollectionInfoView>
    {
        private readonly ICollectionQueryService _service;
        public GetCollectionInfoViewQueryHandler(ICollectionQueryService service)
        {
            _service = service;
        }
        public async Task<CollectionInfoView> Handle(GetCollectionInfoViewQuery request, CancellationToken cancellationToken)
           => await _service.GetCollectionbyId(request.CollectionId);

    }
}
