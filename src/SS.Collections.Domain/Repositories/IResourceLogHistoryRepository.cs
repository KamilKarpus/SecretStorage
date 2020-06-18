using SS.Collections.Domain.History;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SS.Collections.Domain.Repositories
{
    public interface IResourceLogHistoryRepository
    {
        Task<ResourceLogHistory> GetbyResourceId(Guid resourceId);
        Task AddAsync(ResourceLogHistory history);
        Task UpdateAsync(ResourceLogHistory history);
        Task RemoveManybyCollectionId(Guid collectionId);
        Task<List<ResourceLogHistory>> GetManybyCollectionId(Guid id);
        Task Remove(ResourceLogHistory resourceLogHistory);
    }
}
