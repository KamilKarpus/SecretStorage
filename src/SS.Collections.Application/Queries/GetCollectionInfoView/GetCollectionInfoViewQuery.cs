using SS.Collections.Application.Configuration.Queries;
using SS.Collections.Application.ReadModels;
using System;

namespace SS.Collections.Application.GetCollectionInfoView
{
    public class GetCollectionInfoViewQuery : IQuery<CollectionInfoView>
    {
        public Guid CollectionId { get; set; }
    }
}
