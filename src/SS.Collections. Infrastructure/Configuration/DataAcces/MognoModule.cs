using Autofac;
using SS.Collections._Infrastructure.Documents.History;
using SS.Collections.Infrastructure.Documents.Resources;
using SS.Common.Mongo;
using SS.Common.Mongo.Utils;
using System;

namespace SS.Collections.Infrastructure.Configuration.DataAcces
{
    internal class MognoModule : Autofac.Module
    {
        private readonly string _dbConnectionString;
        private readonly string _dbName;
        public MognoModule(string dbConnectionString, string dbName)
        {
            _dbConnectionString = dbConnectionString;
            _dbName = dbName;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MongoConnection>()
                .AsImplementedInterfaces()
                .WithParameter("connection", _dbConnectionString)
                .WithParameter("dbName", _dbName);

            builder.RegisterType<MongoRepository<CollectionDocument, Guid>>()
                .AsImplementedInterfaces();

            builder.RegisterType<MongoQueryClient<CollectionDocument, Guid>>()
                .AsImplementedInterfaces();

            builder.RegisterType<MongoQueryClient<ResourceLogHistoryDocument, Guid>>()
                .AsImplementedInterfaces();

            builder.RegisterType<MongoRepository<ResourceLogHistoryDocument, Guid>>()
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(type => type.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope()
                .FindConstructorsWith(new AllConstructorFinder());

            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(type => type.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope()
                .FindConstructorsWith(new AllConstructorFinder());

            base.Load(builder);
        }
    }
}
