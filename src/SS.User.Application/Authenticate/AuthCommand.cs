using MediatR;
using SS.Users.Application.Configuration.Commands;
using SS.Users.Application.ReadModels;

namespace SS.Users.Application.Authenticate
{
    public class AuthCommand : ICommand<TokenResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
