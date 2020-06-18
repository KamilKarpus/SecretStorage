using System;
using System.Collections.Generic;

namespace SS.Collections.Application.ReadModels
{
    public class CollectionInfoView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<ResourceShortView> Resources { get; set; }
    }
}
