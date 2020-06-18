using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace SS.Organizations.Domain.Roles
{
    public class Role : IEquatable<Role>
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public RoleClaims[] Claims { get; private set; }
        public Role(int id, string name, RoleClaims[] claims)
        {
            Id = id;
            Name = name;
            Claims = claims;
        }

        public static Role Owner => new Role(1, nameof(Owner), new RoleClaims[] { RoleClaims.CanEditCollection, RoleClaims.CanEditOrganization, RoleClaims.CanReadCollection });
        public static Role Admin => new Role(2, nameof(Admin), new RoleClaims[] { RoleClaims.CanReadCollection, RoleClaims.CanEditCollection });
        public static Role User => new Role(3, nameof(User), new RoleClaims[] { RoleClaims.CanReadCollection });

        private static Role[] Roles = { Owner, Admin, User };
        public static Role From(int id)
        {
            return Roles.FirstOrDefault(p => p.Id == id);
        }

        public string[] GetClaims()
        {
            return Claims.Select(p => p.Claim).ToArray();
        }

        public bool Equals([AllowNull] Role other)
            => Id == other.Id;

        public override bool Equals(object obj)
           => this.Equals(obj);

        public static bool operator ==(Role a, Role b)
            => a.Equals(b);
        public static bool operator !=(Role a, Role b)
            => !a.Equals(b);
    }
}
