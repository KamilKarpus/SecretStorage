using System;

namespace SS.Collections.Application.ReadModels
{
    public class ResourceInfoView 
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
        public DateTime ReadedTime { get; set; }
        public string ReadedBy { get; set; }
        public DateTime EditedTime { get; set; }
        public string EditedBy { get; set; }
    }
}
