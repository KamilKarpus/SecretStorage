using MediatR;
using SS.Organizations.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace SS.Organizations.Application.Commands.RemoveOrganization
{
    public class RemoveOrganizationCommandHandler : ICommandHandler<RemoveOrganizationCommand>
    {
        private readonly IOrganizationRepository _repository;
        public RemoveOrganizationCommandHandler(IOrganizationRepository repository)
        {
            _repository = repository;
        }
        public async Task<Unit> Handle(RemoveOrganizationCommand request, CancellationToken cancellationToken)
        {

            var organization = await _repository.GetbyId(request.OrganizationId);
            if(organization != null)
            {
                organization.Remove();
                await _repository.Delete(organization);
            }

            return Unit.Value;
        }
    }
}
