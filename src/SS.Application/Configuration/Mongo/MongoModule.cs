using Autofac;
using MongoDB.Driver;

namespace SS.Application.Configuration.Mongo
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
            builder.Register<IMongoClient>(r =>
                new MongoClient(_databaseConnectionString)
            );
            builder.Register<IMongoDatabase>(r =>
            {
                var client = r.Resolve<IMongoClient>();
                return client.GetDatabase(_dbName);
            });
            base.Load(builder);
        }
    }
}
