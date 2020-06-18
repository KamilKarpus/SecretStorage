using MediatR;
using SS.Organizations.Application.Configuration.Queries;
using SS.Organizations.Application.ReadModels.Organizations;
using SS.Organizations.Domain.Repositories;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SS.Organizations.Application.Queries
{
    public class GetOrganizationInfoQueryHandler : IQueryHandler<GetOrganizationInfoQuery, OrganizationView>
    {
        private readonly IOrganizationRepository _repository;

        public GetOrganizationInfoQueryHandler(IOrganizationRepository repository)
        {
            _repository = repository;
        }
        public async Task<OrganizationView> Handle(GetOrganizationInfoQuery request, CancellationToken cancellationToken)
        {
            var organization = await _repository.GetbyId(request.Id);
            return new OrganizationView()
            {
                Id = organization.Id,
                Name = organization.Name,
                Users = organization.Users.Select(p => new UserView()
                {
                    Id = p.Id,
                    DisplayName = p.DisplayName,
                    Role = p.Role.Name
                }).ToList()
            };
        }

    }
}
