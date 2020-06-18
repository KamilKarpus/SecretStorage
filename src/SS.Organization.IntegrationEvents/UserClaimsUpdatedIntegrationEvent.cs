using SS.Infrastructure.EventBus;
using System;

namespace SS.Organizations.IntegrationEvents
{
    public class UserClaimsUpdatedIntegrationEvent : IntegrationEvent
    {
        public Guid UserId { get; private set; }
        public Guid OrganizationId { get; private set; }
        public string[] Claims { get; private set; }

        public UserClaimsUpdatedIntegrationEvent(Guid userId, Guid organizationId, string[] claims)
        {
            UserId = userId;
            OrganizationId = organizationId;
            Claims = claims;
        }
    }
}
