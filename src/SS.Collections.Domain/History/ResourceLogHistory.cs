using SS.Collections.Domain.DomainEvents;
using SS.Common.BuldingBlocks;
using SS.Common.DomainEvents;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SS.Collections.Domain.History
{
    public class ResourceLogHistory : EventSourcedAggregate
    {
        private HashSet<Log> _logs;
        public Guid ResourceId { get; private set; }
        public Guid CollectionId { get; private set; }
        public IReadOnlyList<Log> Logs => _logs.ToList();

        public ResourceLogHistory(Guid id, Guid resourceId, Guid collectionId,
            HashSet<Log> logs, int version) : this()
        {
            Id = id;
            ResourceId = resourceId;
            CollectionId = collectionId;
            _logs = logs;
            Version = version == 0 ? 0 : version;
        }

        public ResourceLogHistory()
        {
            Register<LoggedEntityCreatedDomainEvent>(Apply);
            Register<LoggedEntityEditedDomainEvent>(Apply);
            Register<LoggedEntityReadedDomainEvent>(Apply);
        }
        public void ApplyEvent(IDomainEvent @event)
        {
            RaiseEvent(@event);
        }
        public static ResourceLogHistory Create()
            => new ResourceLogHistory();
        private void Apply(LoggedEntityCreatedDomainEvent @event)
        {
            Id = Guid.NewGuid();
            ResourceId = @event.ResourceId;
            CollectionId = @event.ResourceId;
            _logs = new HashSet<Log>();
            _logs.Add(new Log(@event.EntityId, @event.DisplayName,
                Status.CreatedId, @event.Time));
        }
        private void Apply(LoggedEntityEditedDomainEvent @event)
        {
            _logs.Add(new Log(@event.EntityId, @event.DisplayName,
                Status.EditedId, @event.Time));
        }

        private void Apply(LoggedEntityReadedDomainEvent @event)
        {
            _logs.Add(new Log(@event.EntityId, @event.DisplayName,
                Status.ReadedId, @event.Time));
        }
    }
}
