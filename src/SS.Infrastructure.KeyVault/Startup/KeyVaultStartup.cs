using Autofac;
using SS.Common.Mongo;
using SS.Common.Mongo.Utils;
using SS.Infrastructure.KeyVault.Mediation;
using SS.Infrastructure.KeyVault.Model;
using SS.Infrastructure.KeyVault.Services;
using System;

namespace SS.Infrastructure.KeyVault.Startup
{
    public static class KeyVaultStartup
    {
        public static void Intilize(string connectionstring, string databaseName)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.Register(p =>
            {
                return new MongoRepository<StorageKey, Guid>(new MongoConnection(connectionstring, databaseName));
            }).AsImplementedInterfaces();
            containerBuilder.RegisterType<KeyVaultService>()
                .AsImplementedInterfaces();
            containerBuilder.RegisterModule(new MediationModule());
            var container = containerBuilder.Build();
            KeyVaultCompositionRoot.SetContainer(container);
        }
    }
}
