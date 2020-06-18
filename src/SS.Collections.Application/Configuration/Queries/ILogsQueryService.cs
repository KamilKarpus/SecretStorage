using SS.Collections.Application.ReadModels;
using SS.Infrastructure.PagginationList;
using System;
using System.Threading.Tasks;

namespace SS.Collections.Application.Configuration.Queries
{
    public interface ILogsQueryService
    {
        Task<PagedList<LogsView>> GetLogsbyResourdeId(Guid resourceId, int pageNumber, int pageSize);
    }
}
