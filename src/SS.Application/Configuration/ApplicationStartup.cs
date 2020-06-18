using Autofac;
using SS.Application.Configuration.Mediation;
using SS.Application.Configuration.Mongo;

namespace SS.Application.Configuration
{
    public class ApplicationStartup
    {
        public static void Initialize(string connection, string dbname)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule(new MongoModule(connection, dbname));
            containerBuilder.RegisterModule(new MediatonModule());
            var container = containerBuilder.Build();

            RijandealCompositionRoot.SetContainer(container);
        }
    }
}
