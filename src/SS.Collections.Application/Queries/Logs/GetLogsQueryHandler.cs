using SS.Collections.Application.Configuration.Queries;
using SS.Collections.Application.ReadModels;
using SS.Infrastructure.PagginationList;
using System.Threading;
using System.Threading.Tasks;

namespace SS.Collections.Application.Logs
{
    public class GetLogsQueryHandler : IQueryHandler<GetLogsQuery, PagedList<LogsView>>
    {
        private readonly ILogsQueryService _service;
        public GetLogsQueryHandler(ILogsQueryService service)
        {
            _service = service;
        }
        public async Task<PagedList<LogsView>> Handle(GetLogsQuery request, CancellationToken cancellationToken)
            => await _service.GetLogsbyResourdeId(request.ResourceId, request.PageNumber, request.PageSize);
    }
}
