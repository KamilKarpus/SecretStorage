
using SS.Organizations.Domain.Roles;
using SS.Organizations.Domain.Rules;
using System;
using Xunit;

namespace SS.Organizations.Domain.Tests.BussinessRulesTests
{
    public class IsUserNotMemberOfOrganizationRuleTests : OrganizationRulesBase
    {
        [Fact]
        public void Should_Be_Not_Broken()
        {
            var users = CreateUsers();
            var userId = Guid.NewGuid();
            var rule = new IsUserNotMemberOfOrganization(users, userId);
            Assert.False(rule.isBroken());
        }

        [Fact]
        public void Should_Be_Broken()
        {
            var users = CreateUsers();
            var userId = Guid.NewGuid();
            users.Add(new User(userId, "test@test.pl", "januszex", Role.User));
            var rule = new IsUserNotMemberOfOrganization(users, userId);
            Assert.True(rule.isBroken());
        }
    }
}
