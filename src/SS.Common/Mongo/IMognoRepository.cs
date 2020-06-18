using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SS.Common.Mongo
{
    public interface IMongoRepository<Document, ID> where Document : DocumentBase<ID>
    {
        Task AddAsync(Document document);
        Task<Document> GetAsync(Expression<Func<Document, bool>> predicate);

        Task Update(Document document);

        Task Delete(Document document);

        Task DeleteMany(Expression<Func<Document, bool>> predicate);
        Task<List<Document>> FindAsync(Expression<Func<Document, bool>> predicate);

    }
}
