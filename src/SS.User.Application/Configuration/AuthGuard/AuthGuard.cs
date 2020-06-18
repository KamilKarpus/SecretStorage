using System;
using System.Threading.Tasks;

namespace SS.Users.Application.Configuration.AuthGuard
{
    public interface IAuthGuard
    {
        Task<bool> DoesUserHaveRights(Guid organizationId, Guid userId, string roleClaim);
    }
}
