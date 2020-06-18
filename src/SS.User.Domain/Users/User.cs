
using SS.Users.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SS.Users.Domain
{
    public class User
    {
        private readonly HashSet<Organization> _organizations;

        public Guid Id { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string DisplayName { get; private set; }

        public IReadOnlyCollection<Organization> Organizations => _organizations;
        public User(Guid id, string email, string password, string displayName, HashSet<Organization> organizations)
        {
            Id = id;
            Email = email;
            Password = password;
            DisplayName = displayName;
            _organizations = organizations;
        }

        public static User Create(Guid id, string email, string password, string displayName)
            => new User(id, email, password, displayName, new HashSet<Organization>());
        public void AddOrganization(Guid id, string[] claims)
        {
            var org = _organizations.FirstOrDefault(x => x.Id == id);
            var organization = new Organization(id, claims);
            if(org == null)
            {
                _organizations.Add(organization);
            }
        }
        public void UpdateOrganizationClaims(Guid organizationId, string[] claims)
        {
            var org = _organizations.FirstOrDefault(x => x.Id == organizationId);
            if(org != null)
            {
                org.UpdateClaims(claims);
            }
        }

        public void RemoveOrganization(Guid organizationId)
        {
            var org = _organizations.FirstOrDefault(x => x.Id == organizationId);
            if(org != null)
            {
                _organizations.Remove(org);
            }
        }
    }
}
