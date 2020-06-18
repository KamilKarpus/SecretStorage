
using SS.Infrastructure.EventBus;
using System;

namespace SS.Users.IntegrationEvents
{
    public class UserRemovedFromOrganizationIntegrationEvent : IntegrationEvent
    {
        public Guid UserId { get; private set; }
        public Guid OrganizationId { get; private set; }

        public UserRemovedFromOrganizationIntegrationEvent(Guid userId, Guid organizationId)
        {
            UserId = userId;
            OrganizationId = organizationId;
        }
    }
}
