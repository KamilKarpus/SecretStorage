using System;
using System.Threading.Tasks;

namespace SS.Infrastructure.GrantStore
{
    public interface IGrantStore
    {
        Task AddAsync(Guid userId, string refreshToken);
        Task<bool> HasUserHaveValidToken(Guid userId);
        Task DeleteUserGrant(Guid userId);
        Task UpdateGrantedToken(Guid userId, string newToken);
        Task<GrantModel> GetTokenInfo(Guid OwnerId);
        Task<GrantModel> GetTokenInfo(string refreshToken);
    }
}