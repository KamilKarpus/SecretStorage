using SS.Infrastructure.EventBus;
using System;

namespace SS.Organizations.IntegrationEvents
{
    public class OrganizationDeletedIntergrationEvent : IntegrationEvent
    {
        public Guid OrganizationId { get; private set; }
        public Guid[] UsersIds { get; private set; }
        public OrganizationDeletedIntergrationEvent(Guid organizationId,
            Guid[] usersId)
        {
            OrganizationId = organizationId;
            UsersIds = usersId;
        }
    }
}
