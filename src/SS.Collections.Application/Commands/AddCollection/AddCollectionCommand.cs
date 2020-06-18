using SS.Collections.Application.Configuration.Commands;
using System;

namespace SS.Collections.Application.AddCollection
{
    public class AddCollectionCommand : ICommand
    {
        public Guid CollectionId { get; private set; }
        public Guid OrganizationId { get; private set; }
        public string Name { get; private set; }
        public AddCollectionCommand(Guid collectionId, Guid organizationId, string name)
        {
            CollectionId = collectionId;
            OrganizationId = organizationId;
            Name = name;
        }
    }
}
