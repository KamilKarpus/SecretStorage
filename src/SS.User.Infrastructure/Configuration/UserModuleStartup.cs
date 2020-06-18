using Autofac;
using Serilog;
using SS.Users.Application.Configuration.Commands;
using SS.Users.Infrastructure.Configuration.Auth;
using SS.Users.Infrastructure.Configuration.DataAccess;
using SS.Users.Infrastructure.Configuration.EventBus;
using SS.Users.Infrastructure.Configuration.Logging;
using SS.Users.Infrastructure.Configuration.Mediation;
using SS.Users.Infrastructure.Configuration.Modules;
using SS.Users.Infrastructure.Configuration.Proccesing;
using SS.Users.Infrastructure.Configuration.Security;

namespace SS.Users.Infrastructure.Configuration
{
    public class UserModuleStartup
    {
        public static void Initialize(string connection, string dbname, string secretKey,
            ILogger logger)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule(new LoggingModule(logger));
            containerBuilder.RegisterModule(new MongoModule(connection, dbname));
            containerBuilder.RegisterModule(new MediationModule());
            containerBuilder.RegisterModule(new SecurityModule());
            containerBuilder.RegisterModule(new AuthModule(secretKey, connection, dbname));
            containerBuilder.RegisterModule(new ProccesingModule());
            containerBuilder.RegisterModule(new InternalClientModule());
            containerBuilder.RegisterModule(new EventBusModule());
            var container = containerBuilder.Build();

            UserCompositionRoot.SetContainer(container);
            InternalClientStartup.Initialize();
            EventBusStartup.Initialize();
        }
    }
}
