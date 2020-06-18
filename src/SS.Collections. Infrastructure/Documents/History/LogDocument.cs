using SS.Collections.Infrastructure.Documents.Resources;
using System;

namespace SS.Collections._Infrastructure.Documents.History
{
    public class LogDocument
    {
        public Guid LogId { get; set; }
        public LoggedEntityDocument Entity { get; set; }
        public int Status { get; set; }
        public DateTime Time { get; set; }
    }
}