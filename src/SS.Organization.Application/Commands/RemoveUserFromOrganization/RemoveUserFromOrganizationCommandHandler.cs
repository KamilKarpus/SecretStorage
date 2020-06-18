using MediatR;
using SS.Organizations.Application.Commands;
using SS.Organizations.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace SS.Organizations.Application.RemoveUserFromOrganization
{
    internal class RemoveUserFromOrganizationCommandHandler : ICommandHandler<RemoveUserFromOrganizationCommand>
    {
        private readonly IOrganizationRepository _repository;
        public RemoveUserFromOrganizationCommandHandler(IOrganizationRepository repository)
        {
            _repository = repository;
        }
        public async Task<Unit> Handle(RemoveUserFromOrganizationCommand request, CancellationToken cancellationToken)
        {
            var organization = await _repository.GetbyId(request.OrganizationId);
            if (organization != null)
            {
                organization.RemoveUser(request.UserIdToDelete);
            }
            await _repository.Update(organization);
            return Unit.Value;
        }
    }
}
