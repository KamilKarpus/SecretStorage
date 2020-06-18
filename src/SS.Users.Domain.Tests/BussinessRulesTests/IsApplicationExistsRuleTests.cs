using SS.Organizations.Domain.Rules;
using Xunit;

namespace SS.Organizations.Domain.Tests.BussinessRulesTests
{
    [Collection("OrganizationRules")]
    public class IsApplicationExistsRuleTests : OrganizationRulesBase
    {
        [Fact]
        public void Should_Be_Broken()
        {
            var apps = CreateApps();
            var rule = new IsApplicationExistsRule("App_Test", apps);
            Assert.True(rule.isBroken());
        }
        [Fact]
        public void Should_Be_Not_Broken()
        {
            var apps = CreateApps();
            var rule = new IsApplicationExistsRule("App_Test88", apps);
            Assert.False(rule.isBroken());
        }
    }
}
