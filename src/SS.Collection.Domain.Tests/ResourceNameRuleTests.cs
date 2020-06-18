using SS.Collections.Domain.BussinessRules;
using System;
using System.Collections.Generic;
using Xunit;

namespace SS.Collections.Domain.Tests
{
    public class ResourceNameRuleTests
    {

        private HashSet<Resource> _resources
            => new HashSet<Resource>()
            {
                new Resource(Guid.NewGuid(), Guid.NewGuid(), "test", "test", null),
                new Resource(Guid.NewGuid(), Guid.NewGuid(), "test2", "test", null),
                new Resource(Guid.NewGuid(), Guid.NewGuid(), "test3", "test", null),
                new Resource(Guid.NewGuid(), Guid.NewGuid(), "test4", "test", null),
            };

        [Fact]
        public void Rule_Should_Be_Broken()
        {
            var rule = new ResourceNameBussinessRule(_resources, "test");
            Assert.True(rule.isBroken());
        }

        [Fact]
        public void Rule_Should_NOT_Be_Broken()
        {
            var rule = new ResourceNameBussinessRule(_resources, "kamil");
            Assert.False(rule.isBroken());
        }

    }
}
