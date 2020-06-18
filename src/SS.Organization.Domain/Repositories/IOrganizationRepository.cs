using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SS.Organizations.Domain.Repositories
{
    public interface IOrganizationRepository
    {
        public Task AddAsync(Organization user);
        Task<Organization> GetbyId(Guid id);
        Task<Organization> GetbyName(string name);
        Task Update(Organization organization);
        Task<List<Organization>> GetManyByIds(Guid[] id);
        Task Delete(Organization organization);
    }
}
