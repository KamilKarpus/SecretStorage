using System;

namespace SS.Users.Infrastructure.Documents
{
    public class OrganizationDocument
    {
        public Guid Id { get; set; }
        public string[] Claims { get; set; }
    }
}
