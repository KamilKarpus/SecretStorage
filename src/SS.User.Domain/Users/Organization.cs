using System;
using System.Linq;

namespace SS.Users.Domain.Users
{
    public class Organization
    {
        public Guid Id { get; private set; }
        public string[] Claims { get; private set;}

        public Organization(Guid id, string[] claims)
        {
            Id = id;
            Claims = claims;
        }
        public void UpdateClaims(string[] claims)
        {
            Claims = claims;
        }
        public bool HasClaim(string claim)
            => Claims.Contains(claim);
    }
}
