using SS.Common.DomainEvents;
using SS.Infrastructure.EventBus;
using SS.Organizations.IntegrationEvents;
using SS.Organizations.Domain.DomainEvents;
using System.Threading.Tasks;

namespace SS.Organizations.Infrastructure.DomainEventsHandlers
{
    public class OrganizationDeletedDomainEventHandler : IDomainEventHandler<OrganizationDeletedDomainEvent>
    {
        private readonly IEventBus _bus;
        public OrganizationDeletedDomainEventHandler(IEventBus bus)
        {
            _bus = bus;
        }
        public async Task Handle(OrganizationDeletedDomainEvent @event)
        {
            await _bus.PublishAsync<OrganizationDeletedIntergrationEvent>(new OrganizationDeletedIntergrationEvent(@event.OrganizationId, @event.UsersIds));
        }
    }
}
