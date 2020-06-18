using SS.Infrastructure.EventBus;
using System;

namespace SS.Users.IntegrationEvents
{
    public class OrganizationDeletedIntergrationEvent : IntegrationEvent
    {
        public Guid OrganizationId { get; private set; }
        public Guid[] UsersIds { get; set; }
        public OrganizationDeletedIntergrationEvent(Guid organizationId,
            Guid[] usersId)
        {
            OrganizationId = organizationId;
            UsersIds = usersId;
        }
    }
}
