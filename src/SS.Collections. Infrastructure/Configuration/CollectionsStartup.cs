using Autofac;
using Serilog;
using SS.Collections._Infrastructure.Configuration.Cryptography;
using SS.Collections._Infrastructure.Configuration.Logging;
using SS.Collections.Infrastructure.Configuration.DataAcces;
using SS.Collections.Infrastructure.Configuration.Mediation;
using SS.Collections.Infrastructure.Configuration.Proccesing;

namespace SS.Collections.Infrastructure.Configuration
{
    public static class CollectionsStartup
    {
        public static void Initialize(string connectionString, string dbName, ILogger logger)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule(new LoggingModule(logger));
            containerBuilder.RegisterModule(new MognoModule(connectionString, dbName));
            containerBuilder.RegisterModule(new MediationModule());
            containerBuilder.RegisterModule(new ProccesingModule());
            containerBuilder.RegisterModule(new CryptographyModule(connectionString, "KeyVault"));
            var container = containerBuilder.Build();
            CollectionsCompositionRoot.SetContainer(container);
        }
    }
}
