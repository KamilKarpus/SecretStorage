using SS.Users.Application.Configuration.AuthGuard;
using SS.Users.Domain.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SS.Users.Infrastructure.AuthGuards
{
    public class AuthGuard : IAuthGuard
    {
        private readonly IUserRepository _repository;
        public AuthGuard(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> DoesUserHaveRights(Guid organizationId, Guid userId, string roleClaim)
        {
            var user = await _repository.GetbyId(userId);
            var organization = user.Organizations.FirstOrDefault(p => p.Id == organizationId);
            return organization.HasClaim(roleClaim);
        }
    }
}
