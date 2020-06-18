using System;

namespace SS.Organizations.Infrastructure.Documents.Organizations
{
    public class UserDocument
    {
        public Guid Id { get;  set; }
        public string Email { get;  set; }
        public string DisplayName { get; set; }
        public RoleDocument Role { get; set; }
    }
}
