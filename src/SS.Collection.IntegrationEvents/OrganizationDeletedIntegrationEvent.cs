using SS.Infrastructure.EventBus;
using System;

namespace SS.Collections.IntegrationEvents
{
    public class OrganizationDeletedIntergrationEvent : IntegrationEvent
    {
        public Guid OrganizationId { get; private set; }

        public OrganizationDeletedIntergrationEvent(Guid organizationId)
        {
            OrganizationId = organizationId;
        }
    }
}
