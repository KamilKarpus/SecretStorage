using SS.Common.DomainEvents;
using System;
using System.Collections.Generic;
using System.Text;

namespace SS.Collections.Domain.DomainEvents
{
    public class LoggedEntityReadedDomainEvent : IDomainEvent
    {
        public Guid ResourceId { get; private set; }
        public Guid CollectionId { get; private set; }

        public Guid EntityId { get; private set; }
        public string DisplayName { get; private set; }
        public DateTime Time { get; private set; }

        public LoggedEntityReadedDomainEvent(Guid resourceId, Guid collectionId,
            Guid entityId, string displayName, DateTime time)
        {
            ResourceId = resourceId;
            CollectionId = collectionId;
            EntityId = entityId;
            DisplayName = displayName;
            Time = time;
        }
    }
}
