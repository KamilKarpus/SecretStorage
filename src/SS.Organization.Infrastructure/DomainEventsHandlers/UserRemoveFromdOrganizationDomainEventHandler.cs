using SS.Common.DomainEvents;
using SS.Infrastructure.EventBus;
using SS.Organizations.IntegrationEvents;
using SS.Organizations.Domain.DomainEvents;
using System.Threading.Tasks;

namespace SS.Organizations.Infrastructure.DomainEventsHandlers
{
    public class UserRemoveFromdOrganizationDomainEventHandler : IDomainEventHandler<UserRemovedFromOrganizationDomainEvent>
    {
        private IEventBus _bus;
        public UserRemoveFromdOrganizationDomainEventHandler(IEventBus bus)
        {
            _bus = bus;
        }
        public async Task Handle(UserRemovedFromOrganizationDomainEvent @event)
        {
            await _bus.PublishAsync<UserRemovedFromOrganizationIntegrationEvent>(new UserRemovedFromOrganizationIntegrationEvent(@event.UserId, @event.OrganizationId));
        }
    }
}
