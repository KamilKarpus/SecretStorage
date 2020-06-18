using SS.Common.DomainEvents;
using System;

namespace SS.Collections.Domain.DomainEvents
{
    public class RemoveResourceDomainEvent : IDomainEvent
    {
        public Guid ResourceId { get; private set; }
        public RemoveResourceDomainEvent(Guid resourceId)
        {
            ResourceId = resourceId;
        }
    }
}
