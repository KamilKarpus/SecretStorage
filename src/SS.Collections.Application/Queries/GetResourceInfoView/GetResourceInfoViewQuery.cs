using SS.Collections.Application.Configuration.Queries;
using SS.Collections.Application.ReadModels;
using System;

namespace SS.Collections.Application.GetResourceInfoView
{
    public class GetResourceInfoViewQuery : IQuery<ResourceInfoView>
    {
        public Guid CollectionId { get; set; }
        public Guid ResourceId { get; set; } 
    }
}
