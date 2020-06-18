using MongoDB.Driver;
using SS.Common.Mongo.Utils;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SS.Common.Mongo
{
    public class MongoQueryClient<T, Id> : IMongoQueryClient<T, Id> where T : DocumentBase<Id>
    {
        private readonly IMongoCollection<T> _documents;
        public MongoQueryClient(IMongoConnection connection)
        {
            _documents = connection.GetCollection<T>(typeof(T).Name.Replace("Document", "").ToLower());
        }
        public async Task<T> Query(Expression<Func<T, bool>> predicate)
            => (await _documents.FindAsync(predicate)).FirstOrDefault();

        public async Task<List<T>> QueryMany(Expression<Func<T, bool>> predicate)
            => await (_documents.Find(predicate).ToListAsync());
        
    }
}
