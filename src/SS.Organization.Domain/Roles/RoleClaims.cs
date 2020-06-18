using System;
using System.Diagnostics.CodeAnalysis;

namespace SS.Organizations.Domain.Roles
{
    public class RoleClaims : IEquatable<RoleClaims>
    {
        public int Id { get; private set; }
        public string Claim { get; private set; }
    
        public RoleClaims(int id, string claim)
        {
            Id = id;
            Claim = claim;
        }
    
        public static RoleClaims CanReadCollection => new RoleClaims(1, nameof(CanReadCollection));
        public static RoleClaims CanEditCollection => new RoleClaims(2, nameof(CanEditCollection));
        public static RoleClaims CanEditOrganization => new RoleClaims(3, nameof(CanEditOrganization));
    
        public bool Equals([AllowNull] RoleClaims other)
            => other.Id == Id && other.Claim == Claim;
    
        public static bool operator ==(RoleClaims a, RoleClaims b)
            => a.Equals(b);
    
        public static bool operator !=(RoleClaims a, RoleClaims b)
            => !a.Equals(b);
    }
}