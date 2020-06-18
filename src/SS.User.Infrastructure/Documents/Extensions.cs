using SS.Users.Domain;
using System.Linq;

namespace SS.Users.Infrastructure.Documents
{
    public static class Extensions
    {
        public static User AsEntity(this UserDocument document)
            => new User(document.Id, document.Email, document.Password, document.DisplayName, 
                document.Organizations.Select(p=> new Domain.Users.Organization(p.Id, p.Claims)).ToHashSet());
        public static UserDocument AsDocument(this User user)
            => new UserDocument
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.Password,
                DisplayName = user.DisplayName,
                Organizations = user.Organizations.Select(p=> new OrganizationDocument()
                {
                   Id = p.Id,
                   Claims = p.Claims
                })?.ToList()
            };
    }
}
