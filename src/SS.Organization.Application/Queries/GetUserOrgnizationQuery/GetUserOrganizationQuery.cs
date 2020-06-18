using SS.Infrastructure.PagginationList;
using SS.Organizations.Application.Configuration.Queries;
using SS.Organizations.Application.ReadModels.Organizations;
using System;

namespace SS.Organizations.Application.Queries
{
    public class GetUserOrganizationQuery : IQuery<PagedList<OrganizationShortView>>
    {
        public Guid UserId { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
