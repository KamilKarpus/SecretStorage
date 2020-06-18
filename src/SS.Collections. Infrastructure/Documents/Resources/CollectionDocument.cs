using SS.Common.Mongo;
using System;
using System.Collections.Generic;

namespace SS.Collections.Infrastructure.Documents.Resources
{
    public class CollectionDocument : DocumentBase<Guid>
    {
        public Guid OrganizationId { get;set; }
        public string Name { get; set; }
        public List<ResourceDocument> Resources { get; set; }
    }
}
