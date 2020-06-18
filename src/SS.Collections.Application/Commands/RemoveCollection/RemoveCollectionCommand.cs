using SS.Collections.Application.Configuration.Commands;
using System;

namespace SS.Collections.Application.Commands.RemoveCollection
{
    public class RemoveCollectionCommand : ICommand
    {
        public Guid CollectionId { get; set; }
    }
}
