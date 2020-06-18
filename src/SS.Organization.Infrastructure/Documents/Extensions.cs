using SS.Organizations.Domain;
using SS.Organizations.Domain.Roles;
using System.Linq;

namespace SS.Organizations.Infrastructure.Documents.Organizations
{
    public static class Extensions
    {
        public static UserDocument ToDocument(this User user)
            => new UserDocument()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Id = user.Id,
                Role = new RoleDocument()
                {
                    Id = user.Role.Id,
                    Name = user.Role.Name,
                    Claims = user.Role.Claims.Select(p=>
                    new RoleClaimsDocument()
                    {
                        Id = p.Id,
                        Claim = p.Claim
                    }).ToArray()
                }
            };
        public static User AsEntity(this UserDocument document)
            => new User(
                document.Id,
                document.Email,
                document.DisplayName,
                new Role(
                    document.Role.Id,
                    document.Role.Name,
                    document.Role.Claims.Select(p =>
                    new RoleClaims(
                        p.Id,
                        p.Claim)).ToArray()
                    ));

        public static ApplicationDocument ToDocument(this App app)
            => new ApplicationDocument()
            {
                Id = app.Id,
                Name = app.Name,
                Key = app.Key
            };
        public static App AsEntity(this ApplicationDocument document)
            => new App(document.Id, document.Name, document.Key);
        public static OrganizationDocument ToDocument(this Organization organization)
            => new OrganizationDocument()
            {
                Id = organization.Id,
                Name = organization.Name,
                Users = organization.Users?.Select(p => p.ToDocument()).ToList(),
                Applications = organization.Applications?.Select(p => p.ToDocument()).ToList()
            };
        public static Organization AsEntity(this OrganizationDocument document)
            => new Organization(document.Id,
                document.Name,
                document.Users.Select(p => p.AsEntity()).ToHashSet(),
                document.Applications.Select(p => p.AsEntity()).ToHashSet());
    }
}
