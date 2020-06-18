using System;
using System.Threading.Tasks;

namespace SS.Users.Domain.Repositories
{
    public interface IUserRepository
    {
        public Task AddAsync(User user);
        Task<User> GetbyEmail(string email);
        Task<User> GetbyId(Guid id);
        Task Update(User user);
    }
}
