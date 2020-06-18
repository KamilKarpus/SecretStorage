using SS.Common.EventDispatchers;
using SS.Common.Mongo;
using SS.Organizations.Domain;
using SS.Organizations.Domain.Repositories;
using SS.Organizations.Infrastructure.Documents.Organizations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SS.Organizations.Infrastructure.Repositories
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly IMongoRepository<OrganizationDocument, Guid> _repository;
        private readonly IEventDispatcher _dispatcher;
        public OrganizationRepository(IMongoRepository<OrganizationDocument, Guid> repository,
            IEventDispatcher dispatcher)
        {
            _repository = repository;
            _dispatcher = dispatcher;
        }
        public async Task AddAsync(Organization organization)
        {
            await _repository.AddAsync(organization.ToDocument());
            await _dispatcher.Dispatch(organization);
        }

        public async Task Delete(Organization organization)
        {
            await _repository.Delete(organization.ToDocument());
            await _dispatcher.Dispatch(organization);
        }

        public async Task<Organization> GetbyId(Guid id)
            => (await _repository.GetAsync(p => p.Id == id))?.AsEntity();

        public async Task<Organization> GetbyName(string name)
           => (await _repository.GetAsync(p => p.Name == name))?.AsEntity();

        public async Task<List<Organization>> GetManyByIds(Guid[] ids)
        {
            List<Organization> organizations = new List<Organization>();
            foreach(var id in ids)
            {
                var organization = await GetbyId(id);
                organizations.Add(organization);
            }
            return organizations;
        }
        public async Task Update(Organization organization)
        {
            await _repository.Update(organization.ToDocument());
            await _dispatcher.Dispatch(organization);
        }

    }
}
