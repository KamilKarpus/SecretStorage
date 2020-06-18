using SS.Common.DomainEvents;
using System;
using System.Collections.Generic;
using System.Text;

namespace SS.Common.BuldingBlocks
{
    public class EventSourcedAggregate
    {
        private readonly Dictionary<Type, Action<IDomainEvent>> _handlers = new Dictionary<Type, Action<IDomainEvent>>();
        public Guid Id { get; protected set; }
        public int Version { get; protected set; }
        protected void Register<T>(Action<T> handle) => _handlers[typeof(T)] = e => handle((T)e);

        protected void RaiseEvent(IDomainEvent @event)
        {
            ExecuteEvent(@event);
        }

        private void ExecuteEvent(IDomainEvent @event)
        {
            _handlers[@event.GetType()](@event);
            Version++;
        }

    }
}
