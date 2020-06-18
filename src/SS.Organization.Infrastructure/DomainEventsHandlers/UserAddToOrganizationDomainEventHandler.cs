using SS.Common.DomainEvents;
using SS.Infrastructure.EventBus;
using SS.Organizations.IntegrationEvents;
using SS.Organizations.Domain.DomainEvents;
using System.Threading.Tasks;

namespace SS.Organizations.Infrastructure.DomainEventsHandlers
{
    public class UserAddToOrganizationDomainEventHandler : IDomainEventHandler<UserAddToOrganizationDomainEvent>
    {
        private readonly IEventBus _bus;
        public UserAddToOrganizationDomainEventHandler(IEventBus bus)
        {
            _bus = bus;
        }
        public async Task Handle(UserAddToOrganizationDomainEvent @event)
        {
            await _bus.PublishAsync<UserAddToOrganizationIntegrationEvent>(new UserAddToOrganizationIntegrationEvent(@event.UserId, @event.OrganizationId, @event.Claims));
        }
        
    }
}
