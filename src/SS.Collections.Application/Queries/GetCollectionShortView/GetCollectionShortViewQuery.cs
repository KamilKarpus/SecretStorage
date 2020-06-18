using SS.Collections.Application.Configuration.Queries;
using SS.Collections.Application.ReadModels;
using SS.Infrastructure.PagginationList;
using System;

namespace SS.Collections.Application.GetCollectionShortView
{
    public class GetCollectionShortViewQuery : IQuery<PagedList<CollectionShortView>>
    { 
        public Guid OrganizationId { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
