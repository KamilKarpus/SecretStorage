using SS.Api;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using SS.Api.Extensions;
using System.IO;
using Xunit.Abstractions;

namespace SS.IntegrationTests
{
    public class SSFixture : WebApplicationFactory<TestStartup>
    {
        // Must be set in each test
        public ITestOutputHelper Output { get; set; }
        protected override IHostBuilder CreateHostBuilder()
        {
            var projectDir = Directory.GetCurrentDirectory();
            var configPath = Path.Combine(projectDir, "appsettings.test.json");
            var builder = Host.CreateDefaultBuilder();
            builder.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.ConfigureWebHostDefaults(webBuilder =>
              {
                  webBuilder.UseStartup<TestStartup>();
                  webBuilder.UseUrls("http://localhost:5555");
                  webBuilder.UseIntegration();
                  webBuilder.UseContentRoot(Directory.GetCurrentDirectory());
                  webBuilder.ConfigureAppConfiguration((builderContext, config) =>
                  {
                      config.AddJsonFile(configPath);
                  });
              });
            return builder;
        }
    }
}
