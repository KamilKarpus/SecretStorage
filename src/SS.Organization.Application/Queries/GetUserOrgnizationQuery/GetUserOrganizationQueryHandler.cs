using SS.Infrastructure.PagginationList;
using SS.Organizations.Application.Configuration.Queries;
using SS.Organizations.Application.Configuration.Services;
using SS.Organizations.Application.ReadModels.Organizations;
using SS.Organizations.Domain.Repositories;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SS.Organizations.Application.Queries
{
    public class GetUserOrganizationQueryHandler : IQueryHandler<GetUserOrganizationQuery, PagedList<OrganizationShortView>>
    {
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IUserService _service;
        public GetUserOrganizationQueryHandler(IOrganizationRepository organizationRepository, IUserService service)
        {
            _organizationRepository = organizationRepository;
            _service = service;
        }
        public async Task<PagedList<OrganizationShortView>> Handle(GetUserOrganizationQuery request, CancellationToken cancellationToken)
        {
            var user = await _service.GetbyId(request.UserId);
            var organizationIds = user.Organizations.Select(p => p.Id).ToArray();
            var organizations = await _organizationRepository.GetManyByIds(organizationIds);
            return organizations.Select(p => new OrganizationShortView()
            {
                Id = p.Id,
                Name = p.Name
            }).ToPagedList(request.PageNumber, request.PageSize);
        }
    }
}
