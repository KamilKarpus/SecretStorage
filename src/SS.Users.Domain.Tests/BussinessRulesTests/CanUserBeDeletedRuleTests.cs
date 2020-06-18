
using SS.Organizations.Domain.Roles;
using SS.Organizations.Domain.Rules;
using System;
using System.Linq;
using Xunit;

namespace SS.Organizations.Domain.Tests.BussinessRulesTests
{
    [Collection("OrganizationRules")]
    public class CanUserBeDeletedRuleTests : OrganizationRulesBase
    {

        [Fact]
        public void Should_Be_Not_Broken()
        {
            var users = CreateUsers();

            var userId = Guid.NewGuid();

            users.Add(new User(userId, "tester@tdd.pl", "testr13", Role.Owner));

            var rule = new CanUserBeDeletedRule(users, userId);
            Assert.False(rule.isBroken()); 
        }

        [Fact]
        public void Should_Be_Broken()
        {
            var users = CreateUsers();

            var userId = Guid.NewGuid();

            users.Add(new User(userId, "tester@tdd.pl", "testr13", Role.User));
            var user = users.FirstOrDefault(p => p.Role == Role.Owner);

            var rule = new CanUserBeDeletedRule(users, user.Id);
            Assert.True(rule.isBroken());
        }


        [Fact]
        public void Should_Be_NOT_Broken_WhenDeleteingRegularUser()
        {
            var users = CreateUsers();

            var userId = Guid.NewGuid();

            users.Add(new User(userId, "tester@tdd.pl", "testr13", Role.User));

            var rule = new CanUserBeDeletedRule(users, userId);
            Assert.False(rule.isBroken());
        }
    }
}
