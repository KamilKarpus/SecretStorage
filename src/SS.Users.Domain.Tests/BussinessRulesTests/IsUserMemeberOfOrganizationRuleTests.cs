using SS.Organizations.Domain.Roles;
using SS.Organizations.Domain.Rules;
using System;
using Xunit;

namespace SS.Organizations.Domain.Tests.BussinessRulesTests
{
    [Collection("OrganizationRules")]
    public class IsUserMemeberOfOrganizationRuleTests : OrganizationRulesBase
    {
        [Fact]
        public void Should_Be_Not_Broken()
        {
            var userId = Guid.NewGuid();
            var users = CreateUsers();
            users.Add(new User(userId, "kamilek@test.pl", "Kamil", Role.User));
            var rule = new IsUserMemeberOfOrganizationRule(users,userId);
            Assert.False(rule.isBroken());
        }

        [Fact]
        public void Should_Be_Broken()
        {
            var userId = Guid.NewGuid();
            var users = CreateUsers();
            var rule = new IsUserMemeberOfOrganizationRule(users, userId);
            Assert.True(rule.isBroken());
        }
    }
}
