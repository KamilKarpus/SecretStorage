using Autofac;
using SS.Common.Mongo;
using SS.Common.Mongo.Utils;
using System;

namespace SS.Users.Infrastructure.Configuration.DataAccess
{
    internal class MongoModule : Autofac.Module
    {
        private readonly string _databaseConnectionString;
        private readonly string _dbName;
        internal MongoModule(string databaseConnectionString, string databaseName)
        {
            _databaseConnectionString = databaseConnectionString;
            _dbName = databaseName;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register<MongoConnection>(r =>
                new MongoConnection(_databaseConnectionString, _dbName)
            ).AsImplementedInterfaces();

            builder.RegisterType<MongoRepository<Documents.UserDocument, Guid>>().AsImplementedInterfaces();
         
            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(type => type.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope()
                .FindConstructorsWith(new AllConstructorFinder());
            base.Load(builder);
        }
    }
}
