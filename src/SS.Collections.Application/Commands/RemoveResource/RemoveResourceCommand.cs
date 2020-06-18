using SS.Collections.Application.Configuration.Commands;
using System;

namespace SS.Collections.Application.Commands.RemoveResource
{
    public class RemoveResourceCommand : ICommand
    {
        public Guid CollectionId { get; set; }
        public Guid ResourceId { get; set; }
    }
}
