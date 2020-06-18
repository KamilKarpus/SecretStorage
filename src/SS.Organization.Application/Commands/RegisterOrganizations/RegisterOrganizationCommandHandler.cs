using MediatR;
using SS.Organizations.Application.Configuration.Services;
using SS.Organizations.Domain;
using SS.Organizations.Domain.Exceptions;
using SS.Organizations.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace SS.Organizations.Application.Commands.Organizations
{
    public class RegisterOrganizationCommandHandler : ICommandHandler<RegisterOrganizationCommand>
    {
        private readonly IOrganizationRepository _repository;
        private readonly IUserService _service; 

        public RegisterOrganizationCommandHandler(IOrganizationRepository repository, IUserService service)
        {
            _repository = repository;
            _service = service;
        }
        public async Task<Unit> Handle(RegisterOrganizationCommand request, CancellationToken cancellationToken)
        {
            var user = await _service.GetbyId(request.UserId);
            if (user == null)
            {
                throw new OrganizationException("User doesn't exists", HttpErrorCodes.ResourceNotFound, InternalErrorCodes.OrganizationDoesntExits);
            }
            var organization = await _repository.GetbyName(request.OrganizationName);
            if (organization != null)
            {
                throw new OrganizationException("Organization name is taken", HttpErrorCodes.ResourceNotFound, InternalErrorCodes.UserDoesntExists);
            }
            var organizationToAdd = Organization.Create(request.OrgnizationId, request.OrganizationName,
                user.Id, user.Email, user.DisplayName);

            await _repository.AddAsync(organizationToAdd);

            return Unit.Value;
        }
    }
}
