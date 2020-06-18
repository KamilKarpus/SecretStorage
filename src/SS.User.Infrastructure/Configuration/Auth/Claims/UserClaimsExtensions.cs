using System;
using System.Security.Claims;

namespace SS.Users.Infrastructure.Configuration.Auth.Claims
{
    public static class UserClaimsExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal claims)
            => Guid.Parse(claims?.FindFirst(UserClaims.Id).Value);
        public static string GetUserDisplayName(this ClaimsPrincipal claims)
            => claims?.FindFirst(UserClaims.DisplayName).Value;
        public static bool HasUsersClaim(this ClaimsPrincipal claims, Guid organizationId, string claim)
        {
            var usersClaims = claims?.FindAll(organizationId.ToString());
            foreach(var user in usersClaims)
            {
                if (user.Value == claim)
                {
                    return true;
                }
            }
            return false;
        }

        
    }
}
