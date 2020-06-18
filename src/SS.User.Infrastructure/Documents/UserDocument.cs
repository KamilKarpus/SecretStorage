using SS.Common.Mongo;
using System;
using System.Collections.Generic;

namespace SS.Users.Infrastructure.Documents
{
    public class UserDocument : DocumentBase<Guid>
    {
        public string Email { get;  set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
        public List<OrganizationDocument> Organizations { get; set; }
    }
}
