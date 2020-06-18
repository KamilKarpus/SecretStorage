using SS.Common.BuldingBlocks;
using SS.Organizations.Domain.DomainEvents;
using SS.Organizations.Domain.Roles;
using SS.Organizations.Domain.Rules;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SS.Organizations.Domain
{
    public class Organization : Entity
    {
        private HashSet<User> _users;
        private HashSet<App> _applications;
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public IReadOnlyCollection<User> Users { get => _users.ToList(); }
        public IReadOnlyCollection<App> Applications { get => _applications.ToList(); }
        public Organization(Guid id, string name, HashSet<User> users,
            HashSet<App> applications)
        {
            _users = users;
            _applications = applications;
            Id = id;
            Name = name;
        }

        public void AddUser(Guid id, string email, string displayName)
        {
            CheckRule(new IsUserNotMemberOfOrganization(_users, id));
            AddDomainEvent(new UserAddToOrganizationDomainEvent(id, Id, Role.User.GetClaims()));
            var user = new User(id, email, displayName, Role.User);
            _users.Add(user);
        }
        public void AddApplication(Guid id, string name, string key)
        {
            CheckRule(new IsApplicationExistsRule(name, _applications));

            var application = new App(id, name, key);
            _applications.Add(application);
        }

        public static Organization Create(Guid id, string name, Guid ownerId, string email, string displayName)
        {
            var users = new HashSet<User>();
            users.Add(new User(ownerId, email, displayName, Role.Owner));
            var apps = new HashSet<App>();
            var organization = new Organization(id, name, users, apps);
            organization.AddDomainEvent(new UserAddToOrganizationDomainEvent(ownerId, id, Role.Owner.GetClaims()));
            return organization;
        }

        public void ChangeUserRole(Guid userId, int roleId)
        {
            var role = Role.From(roleId);

            CheckRule(new IsUserMemeberOfOrganizationRule(_users, userId));
            CheckRule(new CanUserBeChangedRule(_users, userId, role));

            var user = _users.FirstOrDefault(p => p.Id == userId);
            user.ChangeRole(role);
            AddDomainEvent(new UserClaimsUpdatedDomainEvent(user.Id, Id, user.Role.GetClaims()));
        }

        public void RemoveUser(Guid userId)
        {
            CheckRule(new IsUserMemeberOfOrganizationRule(_users, userId));
            CheckRule(new CanUserBeDeletedRule(_users, userId));

            var user = _users.FirstOrDefault(p => p.Id == userId);
            _users.Remove(user);
            AddDomainEvent(new UserRemovedFromOrganizationDomainEvent(userId, Id));
        }

        public void Remove()
        {
            var guids = _users.Select(p => p.Id).ToArray();
            AddDomainEvent(new OrganizationDeletedDomainEvent(Id, guids));
        }
    }
}
