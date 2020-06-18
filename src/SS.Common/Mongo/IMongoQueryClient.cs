using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SS.Common.Mongo
{
    public interface IMongoQueryClient<T, ID> where T : DocumentBase<ID>
    {
        Task<T> Query(Expression<Func<T, bool>> predicate);
        Task<List<T>> QueryMany(Expression<Func<T, bool>> predicate);
    }
}
