using SS.Common.Mongo;
using System;
using System.Collections.Generic;

namespace SS.Organizations.Infrastructure.Documents.Organizations
{
    public class OrganizationDocument : DocumentBase<Guid>
    {
        public string Name { get; set; }
        public List<UserDocument> Users {get; set;}
        public List<ApplicationDocument> Applications { get; set; }

    }
}
