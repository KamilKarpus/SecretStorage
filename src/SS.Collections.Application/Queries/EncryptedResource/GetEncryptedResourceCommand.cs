using SS.Collections.Application.Configuration.Commands;
using SS.Collections.Application.ReadModels;
using System;

namespace SS.Collections.Application.EncryptedResource
{
    public class GetEncryptedResourceCommand : ICommand<EncryptedValue>
    {
        public Guid CollectionId { get; set; }
        public Guid ResourceId { get; set; }
        public Guid UserId { get; set; }
        public string DisplayName { get; set; }
    }
}
