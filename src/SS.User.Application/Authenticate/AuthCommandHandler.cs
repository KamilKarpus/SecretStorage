using SS.Users.Application.Configuration.Commands;
using SS.Users.Application.ReadModels;
using SS.Users.Domain.Exceptions;
using SS.Users.Domain.Repositories;
using SS.Users.Domain.Security;
using System.Threading;
using System.Threading.Tasks;

namespace SS.Users.Application.Authenticate
{
    public class AuthCommandHandler : ICommandHandler<AuthCommand,TokenResponse>
    {
        private readonly IUserRepository _repository;
        private readonly IAuthenticateService _service;
        private readonly IPasswordHasher _hasher;
        public AuthCommandHandler(IAuthenticateService service,
            IUserRepository repository, IPasswordHasher hasher)
        {
            _service = service;
            _repository = repository;
            _hasher = hasher;
        }

        public async Task<TokenResponse> Handle(AuthCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetbyEmail(request.Email);
            if(user == null)
            {
                throw new UserException("User not found.", HttpCodes.ResourceNotFound, ErrorCodes.UserNotFound);
            }
            if (!_hasher.Check(user.Password, request.Password))
            {
                throw new UserException("Password doesn't match.", HttpCodes.PasswordMatch, ErrorCodes.PasswordMatch);
            }
            return await _service.GenerateCreditinial(user);
           
        }
    }
}
