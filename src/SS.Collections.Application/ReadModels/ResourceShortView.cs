using System;

namespace SS.Collections.Application.ReadModels
{
    public class ResourceShortView
    {
        public Guid Id { get; set; }
        public string Name { get;  set; }
        public DateTime ReadedTime { get; set; }
        public DateTime EditedTime { get; set; }
    }
}
