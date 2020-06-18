using SS.Common.Mongo;
using SS.Users.Domain;
using SS.Users.Domain.Repositories;
using SS.Users.Infrastructure.Documents;
using System;
using System.Threading.Tasks;

namespace SS.Users.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoRepository<UserDocument, Guid> _repository;
        public UserRepository(IMongoRepository<UserDocument, Guid> repository)
        {
            _repository = repository;
        }
        public async Task AddAsync(User user)
            => await _repository.AddAsync(user.AsDocument());

        public async Task<User> GetbyEmail(string email)
            => (await _repository.GetAsync(p => p.Email == email))?.AsEntity();

        public async Task<User> GetbyId(Guid id)
             => (await _repository.GetAsync(p => p.Id == id))?.AsEntity();

        public async Task Update(User user)
            => await _repository.Update(user.AsDocument());

    }
}
