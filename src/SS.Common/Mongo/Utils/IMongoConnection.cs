using MongoDB.Driver;

namespace SS.Common.Mongo.Utils
{
    public interface IMongoConnection
    {
        IMongoCollection<T> GetCollection<T>(string name);
    }
}
