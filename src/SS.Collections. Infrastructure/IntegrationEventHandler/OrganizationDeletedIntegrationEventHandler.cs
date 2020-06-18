using MediatR;
using SS.Collections.Domain.Repositories;
using SS.Collections.IntegrationEvents;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SS.Collections._Infrastructure.IntegrationEventHandler
{
    public class OrganizationDeletedIntegrationEventHandler : INotificationHandler<OrganizationDeletedIntergrationEvent>
    {
        private readonly ICollectionRepository _repository;
        public OrganizationDeletedIntegrationEventHandler(ICollectionRepository repository)
        {
            _repository = repository;
        }
        public async Task Handle(OrganizationDeletedIntergrationEvent notification, CancellationToken cancellationToken)
        {
            var collection = await _repository.GetbyOrganizationId(notification.OrganizationId);
            if(collection != null)
            {
                await _repository.Delete(collection);
            }
        }
    }
}
