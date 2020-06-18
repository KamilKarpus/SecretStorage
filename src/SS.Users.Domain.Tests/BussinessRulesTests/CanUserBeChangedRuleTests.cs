
using SS.Organizations.Domain.Roles;
using SS.Organizations.Domain.Rules;
using System;
using Xunit;

namespace SS.Organizations.Domain.Tests.BussinessRulesTests
{
    [Collection("OrganizationRules")]
    public class CanUserBeChangedRuleTests : OrganizationRulesBase
    {
        [Fact]
        public void Should_Be_Broken()
        {
            var userId = Guid.NewGuid();
            var users = CreateUsers();
            users.Add(new User(userId, "tester@test.pl", "tessss", Role.User));
            var rule = new CanUserBeChangedRule(users,userId, Role.User);
            Assert.True(rule.isBroken());
        }

        [Fact]
        public void Should_Be_Not_Broken()
        {
            var userId = Guid.NewGuid();
            var users = CreateUsers();
            users.Add(new User(userId, "tester@test.pl", "tessss", Role.User));
            var rule = new CanUserBeChangedRule(users, userId, Role.Admin);
            Assert.False(rule.isBroken());
        }

    }
}
