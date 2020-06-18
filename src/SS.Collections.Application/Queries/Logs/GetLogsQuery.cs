using SS.Collections.Application.Configuration.Queries;
using SS.Collections.Application.ReadModels;
using SS.Infrastructure.PagginationList;
using System;

namespace SS.Collections.Application.Logs
{
    public class GetLogsQuery : IQuery<PagedList<LogsView>>
    {
        public Guid ResourceId { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
