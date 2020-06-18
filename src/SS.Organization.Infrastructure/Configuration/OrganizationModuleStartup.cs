using Autofac;
using Serilog;
using SS.Organizations.Infrastructure.Configuration.DataAccess;
using SS.Organizations.Infrastructure.Configuration.EventBus;
using SS.Organizations.Infrastructure.Configuration.InternalClient;
using SS.Organizations.Infrastructure.Configuration.Logging;
using SS.Organizations.Infrastructure.Configuration.Mediation;
using SS.Organizations.Infrastructure.Configuration.Proccesing;

namespace SS.Organizations.Infrastructure.Configuration
{
    public class OrganizationModuleStartup
    {
        public static void Initialize(string connection, string dbname, ILogger logger)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule(new LoggingModule(logger));
            containerBuilder.RegisterModule(new MongoModule(connection, dbname));
            containerBuilder.RegisterModule(new MediationModule());
            containerBuilder.RegisterModule(new ProccesingModule());
            containerBuilder.RegisterModule(new InternalClientModule());
            containerBuilder.RegisterModule(new EventBusModule());
            var container = containerBuilder.Build();

            OrganizationCompositionRoot.SetContainer(container);
        }
    }
}
