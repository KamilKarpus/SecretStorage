using MongoDB.Driver;
namespace SS.Common.Mongo.Utils
{
    public class MongoConnection : IMongoConnection
    {
        private IMongoDatabase _db;
        private MongoClient _mongoClient;
        public MongoConnection(string connection, string dbName)
        {
            _mongoClient = new MongoClient(connection);
            _db = _mongoClient.GetDatabase(dbName);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _db.GetCollection<T>(name);
        }
    }
}
