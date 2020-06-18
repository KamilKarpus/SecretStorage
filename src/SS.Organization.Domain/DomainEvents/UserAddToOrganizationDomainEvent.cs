using SS.Common.DomainEvents;
using System;

namespace SS.Organizations.Domain.DomainEvents
{
    public class UserAddToOrganizationDomainEvent : IDomainEvent
    {
        public Guid UserId { get; private set; }
        public Guid OrganizationId { get; private set; }
        public string[] Claims { get; private set; }

        public UserAddToOrganizationDomainEvent(Guid userId, Guid organizationId, string[] claims)
        {
            UserId = userId;
            OrganizationId = organizationId;
            Claims = claims;
        }
    }
}
