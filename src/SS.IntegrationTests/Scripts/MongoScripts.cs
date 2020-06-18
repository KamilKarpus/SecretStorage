using MongoDB.Driver;
using SS.Users.Infrastructure.Documents;
using System;

namespace SS.IntegrationTests.Scripts
{
    public class MongoScripts
    {
        private readonly MongoClient _client;
        public MongoScripts(string connectionString)
        {
            _client = new MongoClient(connectionString);
        }

        public void DropDatabase(string dbName)
        {
            _client.DropDatabase(dbName);
        }
    }
}
