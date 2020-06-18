using System;
using System.Threading.Tasks;

namespace SS.Collections.Domain.Repositories
{
    public interface ICollectionRepository
    {
        Task AddAsync(Collection collection);
        Task UpdateAsync(Collection collection);
        Task<Collection> GetbyId(Guid id);
        Task Delete(Collection collection);
        Task<Collection> GetbyOrganizationId(Guid id);
        Task<Collection> GetbyName(string name);
    }
}
