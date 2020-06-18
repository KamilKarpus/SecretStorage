using MongoDB.Bson;
using MongoDB.Driver;
using SS.Common.Mongo.Utils;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SS.Common.Mongo
{
    public class MongoRepository<Document, Id> : IMongoRepository<Document, Id>
        where Document : DocumentBase<Id>
    {
        private readonly IMongoCollection<Document> _documents;
        public MongoRepository(IMongoConnection connection)
        {
            _documents = connection.GetCollection<Document>(typeof(Document).Name.Replace("Document", "").ToLower());
        }
        public async Task AddAsync(Document document)
            => await _documents.InsertOneAsync(document);
        public async Task<Document> GetAsync(Expression<Func<Document, bool>> predicate)
            => await _documents.Find(predicate).SingleOrDefaultAsync();

        public async Task Update(Document document)
            => await _documents.ReplaceOneAsync(p => p.Id.Equals(document.Id), document);

        public async Task<List<Document>> FindAsync(Expression<Func<Document, bool>> predicate)
            => await _documents.Find(predicate).ToListAsync();

        public async Task Delete(Document document)
           => await _documents.DeleteOneAsync(p => p.Id.Equals(document.Id));

        public async Task DeleteMany(Expression<Func<Document, bool>> predicate)
            => await _documents.DeleteManyAsync<Document>(predicate);
    }
}
