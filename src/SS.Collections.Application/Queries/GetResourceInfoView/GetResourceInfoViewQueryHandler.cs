using SS.Collections.Application.Configuration.Queries;
using SS.Collections.Application.ReadModels;
using System.Threading;
using System.Threading.Tasks;

namespace SS.Collections.Application.GetResourceInfoView
{
    internal class GetResourceInfoViewQueryHandler : IQueryHandler<GetResourceInfoViewQuery, ResourceInfoView>
    {
        private readonly ICollectionQueryService _service;
        public GetResourceInfoViewQueryHandler(ICollectionQueryService service)
        {
            _service = service;
        }
        public async Task<ResourceInfoView> Handle(GetResourceInfoViewQuery request, CancellationToken cancellationToken)
            => await _service.GetResourcebyId(request.CollectionId, request.ResourceId);
    }
}
