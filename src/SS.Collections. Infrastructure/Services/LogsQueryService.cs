using SS.Collections._Infrastructure.Documents.History;
using SS.Collections.Application.Configuration.Queries;
using SS.Collections.Application.ReadModels;
using SS.Collections.Domain;
using SS.Common.Mongo;
using SS.Infrastructure.PagginationList;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SS.Collections._Infrastructure.Services
{
    internal class LogsQueryService : ILogsQueryService
    {
        private readonly IMongoQueryClient<ResourceLogHistoryDocument, Guid> _client;
        public LogsQueryService(IMongoQueryClient<ResourceLogHistoryDocument, Guid> client)
        {
            _client = client;
        }
        public async Task<PagedList<LogsView>> GetLogsbyResourdeId(Guid resourceId, int pageNumber, int pageSize)
            => (await _client.Query(p => p.ResourceId == resourceId))
                    .Logs.Select(p => new LogsView
                    {
                        AccesorName = p.Entity.DisplayName,
                        LogId = p.LogId,
                        Status = Status.From(p.Status).Name,
                        StatusId = p.Status,
                        Time = p.Time
                    }).OrderByDescending(p=>p.Time).ToPagedList(pageNumber, pageSize);
                    

                
    }
}
