using SS.Api;
using Microsoft.Extensions.Configuration;
using SS.IntegrationTests.Scripts;

namespace SS.IntegrationTests
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration)
        {
            var dbScript = new MongoScripts(configuration["Database:ConnectionString"]);
            dbScript.DropDatabase(configuration["Database:DbName"]);
        }


    }
}
