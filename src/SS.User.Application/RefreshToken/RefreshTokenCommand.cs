
using SS.Users.Application.Configuration.Commands;
using SS.Users.Application.ReadModels;

namespace SS.Users.Application.RefreshToken
{
    public class RefreshTokenCommand : ICommand<RefreshTokenResponse>
    {
        public string RefreshToken { get; set; }
    }
}
