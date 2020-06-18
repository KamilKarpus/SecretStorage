using MediatR;
using SS.Organizations.Application.Configuration.Services;
using SS.Organizations.Domain.Exceptions;
using SS.Organizations.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace SS.Organizations.Application.Commands.AddUserToOrganization
{
    public class AddUserToOrganizationCommandHandler : ICommandHandler<AddUserToOrganizationCommand>
    {
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IUserService _service;
        public AddUserToOrganizationCommandHandler(IOrganizationRepository repository, IUserService service)
        {
            _organizationRepository = repository;
            _service = service;
            
        }
        public async Task<Unit> Handle(AddUserToOrganizationCommand request, CancellationToken cancellationToken)
        {
            var user = await _service.GetbyEmail(request.Email);
            if (user == null)
            {
                throw new OrganizationException("User doesn't exists", HttpErrorCodes.ResourceNotFound, InternalErrorCodes.UserDoesntExists);
            }

            var organization = await _organizationRepository.GetbyId(request.OrganizationId);
            if (organization == null)
            {
                throw new OrganizationException("Organization doesn't exists", HttpErrorCodes.ResourceNotFound,InternalErrorCodes.OrganizationDoesntExits);
            }
            organization.AddUser(user.Id, user.Email, user.DisplayName);
            await _organizationRepository.Update(organization);
            return Unit.Value;    
        }
    }
}
