using SS.Common.DomainEvents;
using System;

namespace SS.Organizations.Domain.DomainEvents
{
    public class OrganizationDeletedDomainEvent : IDomainEvent
    {
        public Guid OrganizationId { get; private set; }
        public Guid[] UsersIds { get; set; }
        public OrganizationDeletedDomainEvent(Guid organizationId,
            Guid[] usersId)
        {
            OrganizationId = organizationId;
            UsersIds = usersId;
        }
    }
}
