using System;

namespace SS.Collections.Infrastructure.Documents.Resources
{
    public class ResourceDocument
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public LoggedEntityDocument Owner { get; set; }
        public byte[] EncryptedResource { get; set; }
        public DateTime ReadedTime { get; set; }
        public LoggedEntityDocument ReadedBy { get; set; }
        public DateTime EditedTime { get; set; }
        public LoggedEntityDocument EditedBy { get; set; }
    }
}
