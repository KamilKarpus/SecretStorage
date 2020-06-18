using MediatR;
using SS.Organizations.Application.Configuration.Services;
using SS.Organizations.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace SS.Organizations.Application.Commands.ChangeUserRole
{
    public class ChangeUserRoleCommandHandler : ICommandHandler<ChangeUserRoleCommand>
    {
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IUserService _service;
        public ChangeUserRoleCommandHandler(IOrganizationRepository organizationRepository,
            IUserService service)
        {
            _organizationRepository = organizationRepository;
            _service = service;
        }
        public async Task<Unit> Handle(ChangeUserRoleCommand request, CancellationToken cancellationToken)
        {
            var organization = await _organizationRepository.GetbyId(request.OrganizationId);
            organization.ChangeUserRole(request.UserId, request.RoleId);
            await _organizationRepository.Update(organization);
            return Unit.Value;
        }
    }
}
