using SS.Organizations.Application.Configuration.Queries;
using SS.Organizations.Application.ReadModels.Organizations;
using System;

namespace SS.Organizations.Application.Queries
{
    public class GetOrganizationInfoQuery : IQuery<OrganizationView>
    {
        public Guid Id { get; set; }
    }
}
