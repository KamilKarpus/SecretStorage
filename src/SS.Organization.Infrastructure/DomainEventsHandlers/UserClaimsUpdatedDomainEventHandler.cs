using SS.Common.DomainEvents;
using SS.Infrastructure.EventBus;
using SS.Organizations.IntegrationEvents;
using SS.Organizations.Domain.DomainEvents;
using System.Threading.Tasks;

namespace SS.Organizations.Infrastructure.DomainEventsHandlers
{
    public class UserClaimsUpdatedDomainEventHandler : IDomainEventHandler<UserClaimsUpdatedDomainEvent>
    {
        private readonly IEventBus _bus;
        public UserClaimsUpdatedDomainEventHandler(IEventBus bus)
        {
            _bus = bus;
        }
        public async Task Handle(UserClaimsUpdatedDomainEvent @event)
        {
            await _bus.PublishAsync<UserClaimsUpdatedIntegrationEvent>(new UserClaimsUpdatedIntegrationEvent(@event.UserId, @event.OrganizationId, @event.Claims));
        }
    }
}
