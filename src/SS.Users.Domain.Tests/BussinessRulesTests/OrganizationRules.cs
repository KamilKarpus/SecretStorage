
using SS.Organizations.Domain.Roles;
using System;
using System.Collections.Generic;

namespace SS.Organizations.Domain.Tests.BussinessRulesTests
{
    public class OrganizationRulesBase
    {
        protected HashSet<User> CreateUsers()
        => new HashSet<User>()
        {
                new User(Guid.NewGuid(),"januszex@test.pl","januszex",Role.Owner),
                new User(Guid.NewGuid(),"janus2@test.pl","janu2",Role.User),
                new User(Guid.NewGuid(),"michalek12w34@test.pl","janu3",Role.User),
                new User(Guid.NewGuid(),"admin@test.pl","janu4",Role.Admin)
        };

        protected HashSet<App> CreateApps()
            => new HashSet<App>()
            {
                new App(Guid.NewGuid(), "App_Test","123231"),
                new App(Guid.NewGuid(), "App_Test2","123231"),
                new App(Guid.NewGuid(), "App_Test3","123231"),
                new App(Guid.NewGuid(), "App_Test4","123231")
            };
    }
}
