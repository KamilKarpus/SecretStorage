using System;

namespace SS.Infrastructure.EventBus
{
    public class IntegrationEvent : IIntegrationEvent
    {
        public Guid Id { get; set; }
        public DateTime OccurOn { get; set; }

        public IntegrationEvent()
        {
            Guid Id = Guid.NewGuid();
            OccurOn = DateTime.Now;
        }
    }
}
