using SS.Users.Application.Authenticate;
using SS.Users.Application.Configuration.Commands;
using SS.Users.Application.ReadModels;
using System.Threading;
using System.Threading.Tasks;

namespace SS.Users.Application.RefreshToken
{
    public class RefreshTokenCommnadHandler : ICommandHandler<RefreshTokenCommand, RefreshTokenResponse>
    {
        private readonly IAuthenticateService _service;
        public RefreshTokenCommnadHandler(IAuthenticateService service)
        {
            _service = service;
        }
        public async Task<RefreshTokenResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
         =>   await _service.RefreshToken(request.RefreshToken);
    }
}
