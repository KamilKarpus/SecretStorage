using SS.Common.Mongo;
using System;
using System.Collections.Generic;

namespace SS.Collections._Infrastructure.Documents.History
{
    public class ResourceLogHistoryDocument : DocumentBase<Guid>
    {
        public List<LogDocument> Logs { get; set; }
        public Guid ResourceId { get; set; }
        public Guid CollectionId { get; set; }
        public int Version { get; set; }
    }
}
