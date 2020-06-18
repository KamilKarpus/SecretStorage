using SS.Infrastructure.EventBus;
using System;

namespace SS.Organizations.IntegrationEvents
{
    public class UserAddToOrganizationIntegrationEvent : IntegrationEvent
    {
        public Guid UserId { get; private set; }
        public Guid OrganizationId { get; private set; }
        public string[] Claims { get; private set; }

        public UserAddToOrganizationIntegrationEvent(Guid userId, Guid organizationId, string[] claims)
        {
            UserId = userId;
            OrganizationId = organizationId;
            Claims = claims;
        }
    }
}
