using SS.Organizations.Domain.Roles;
using System;

namespace SS.Organizations.Domain
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Email { get; private set; }
        public string DisplayName { get; private set; }
        public Role Role { get; private set; }
        public User(Guid id, string email, string displayName, Role role)
        {
            Id = id;
            Email = email;
            DisplayName = displayName;
            Role = role;
        }

        public void ChangeRole(Role role)
        {
            Role = role;
        }
    }
}
