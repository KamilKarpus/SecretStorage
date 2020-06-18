using SS.Common.DomainEvents;
using System;

namespace SS.Organizations.Domain.DomainEvents
{
    public class UserRemovedFromOrganizationDomainEvent : IDomainEvent
    {
        public Guid UserId { get; private set; }
        public Guid OrganizationId { get; private set; }

        public UserRemovedFromOrganizationDomainEvent(Guid userId, Guid organizationId)
        {
            UserId = userId;
            OrganizationId = organizationId;
        }
    }
}
