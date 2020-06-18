using SS.Common.DomainEvents;
using System.Collections.Generic;

namespace SS.Common.BuldingBlocks
{
    public class Entity
    {
        private List<IDomainEvent> _domainEvents = new List<IDomainEvent>();

        protected void AddDomainEvent(IDomainEvent @event)
           => _domainEvents.Add(@event);

        public IReadOnlyCollection<IDomainEvent> DomainEvents
            => _domainEvents;
        protected void CheckRule(IBusinessRule rule)
        {
            if (rule.isBroken())
            {
                throw rule.Exception;
            }
        }

        public void ClearEvents()
            => _domainEvents.Clear();
    }
}
