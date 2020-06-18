using Microsoft.AspNetCore.Hosting;

namespace SS.Api.Extensions
{
    public static class Extensions
    {
        public static bool IsIntegration(this IWebHostEnvironment environment)
            => environment.EnvironmentName == "Integration";


        public static IWebHostBuilder UseIntegration(this IWebHostBuilder host)
            => host.UseEnvironment("Integration");
    }
}
