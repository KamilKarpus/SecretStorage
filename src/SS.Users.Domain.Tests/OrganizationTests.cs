using SS.Organizations.Domain.Exceptions;
using SS.Organizations.Domain.Roles;
using System;
using System.Linq;
using Xunit;

namespace SS.Organizations.Domain.Tests
{
    public class OrganizationTests
    {
        private Organization CreateOrganization()
            => Organization.Create(Guid.NewGuid(), "test", Guid.NewGuid(), "test@test.pl", "Janusz");
        [Fact]
        public void CTOR_ShoudCreate_WithOwner()
        {
            var ownerId = Guid.NewGuid();
            var organizationId = Guid.NewGuid();
            var email = "users@test.com";
            var organization = Organization.Create(organizationId, "test", ownerId, email, "Janusz");

            var user = organization.Users.FirstOrDefault(p => p.Id == ownerId);

            Assert.Equal(organizationId, organization.Id);
            Assert.Equal(email, user.Email);
            Assert.Equal(ownerId, user.Id);
            Assert.Equal(Role.Owner, user.Role);

        }

        [Fact]
        public void AddUser_Should_AddNewUser()
        {
            var organization = CreateOrganization();
            var userId = Guid.NewGuid();
            var email = "przemke@tdddd.pl";
            var displayName = "DisplayName";
            organization.AddUser(userId, email, displayName);
            var user = organization.Users.FirstOrDefault(p => p.Id == userId);
            Assert.Equal(email, user.Email);
            Assert.Equal(displayName, user.DisplayName);
            Assert.Equal(userId, user.Id);
            Assert.Equal(user.Role, Role.User);
            Assert.NotNull(organization.DomainEvents);
        }
        [Fact]
        public void AddUser_Should_ThrowException()
        {
            var organization = CreateOrganization();
            var userId = Guid.NewGuid();
            var email = "przemke@tdddd.pl";
            var displayName = "DisplayName";
            organization.AddUser(userId, email, displayName);

            Assert.Throws<OrganizationException>(() => organization.AddUser(userId, email, displayName));
        }

        [Theory]
        [InlineData("fefebdc4-8ea7-4a8b-bccf-d7642cbe563d",2)]
        [InlineData("60180b14-e987-499f-8b58-d25aad2d0405", 1)]
        public void ChangeUserRole_Should_ChangeRole(string id, int expectedRoleId)
        {
            var organization = CreateOrganization();
            var userId = Guid.Parse(id);
            var email = "przemke@tdddd.pl";
            var displayName = "DisplayName";
            organization.AddUser(userId, email, displayName);
            organization.ChangeUserRole(userId, expectedRoleId);

            var roleId = organization.Users.FirstOrDefault(p => p.Id == userId).Role.Id;

            Assert.NotNull(organization.DomainEvents);
            Assert.Equal(expectedRoleId, roleId);

        }
        [Theory]
        [InlineData("fefebdc4-8ea7-4a8b-bccf-d7642cbe563d", 2)]
        [InlineData("60180b14-e987-499f-8b58-d25aad2d0405", 3)]
        public void ChangeUserRole_Should_ChangeRoleDown(string id, int expectedRoleId)
        {
            var organization = CreateOrganization();
            var userId = Guid.Parse(id);
            var email = "przemke@tdddd.pl";
            var displayName = "DisplayName";
            organization.AddUser(userId, email, displayName);
            organization.ChangeUserRole(userId, Role.Owner.Id);
            organization.ChangeUserRole(userId, expectedRoleId);
            var roleId = organization.Users.FirstOrDefault(p => p.Id == userId).Role.Id;

            Assert.NotNull(organization.DomainEvents);
            Assert.Equal(expectedRoleId, roleId);
        }

        [Fact]
        public void ChageUserRole_ShoudThrowException_SameRole()
        {
            var organization = CreateOrganization();
            var userId = Guid.NewGuid();
            var email = "przemke@tdddd.pl";
            var displayName = "DisplayName";
            organization.AddUser(userId, email, displayName);

            Assert.Throws<RoleException>(()=>organization.ChangeUserRole(userId, Role.User.Id));
        }
        [Fact]
        public void ChageUserRole_ShouldThrowException_UserIsNotMember()
        {
            var organization = CreateOrganization();
            var userId = Guid.NewGuid();
            var email = "przemke@tdddd.pl";
            var displayName = "DisplayName";
            organization.AddUser(userId, email, displayName);

            Assert.Throws<OrganizationException>(() => organization.ChangeUserRole(Guid.NewGuid(), Role.User.Id));
        }

        [Fact]
        public void AddApp_Should_AddNewApp()
        {
            var appId = Guid.NewGuid();
            var organization = CreateOrganization();
            var name = "Test12345";
            var key = "Key1215455";
            organization.AddApplication(appId, name, key);
            var app = organization.Applications.FirstOrDefault(p => p.Id == appId);

            Assert.Equal(appId, app.Id);
            Assert.Equal(name, app.Name);
            Assert.Equal(key, app.Key);
        }
        [Fact]
        public void AddApp_Shoud_ThrowException()
        {
            var appId = Guid.NewGuid();
            var organization = CreateOrganization();
            var name = "Test12345";
            var key = "Key1215455";
            organization.AddApplication(appId, name, key);

            Assert.Throws<AppException>(() => organization.AddApplication(appId, name, key));
        }
    }
}
