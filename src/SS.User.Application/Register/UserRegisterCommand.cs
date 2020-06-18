using SS.Users.Application.Configuration.Commands;
using System;

namespace SS.Users.Application.Register
{
    public class UserRegisterCommand : ICommand
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
    }
}
