using SS.Common.DomainEvents;
using System;

namespace SS.Collections.Domain.DomainEvents
{
    public class CollectionRemovedDomainEvent : IDomainEvent
    {
        public Guid CollectionId { get; private set; }
        public CollectionRemovedDomainEvent(Guid collectionId)
        {
            CollectionId = collectionId;
        }
    }
}
