using SS.Collections.Domain.DomainEvents;
using SS.Collections.Domain.Repositories;
using SS.Common.DomainEvents;
using System.Threading.Tasks;

namespace SS.Collections._Infrastructure.DomainEventHandlers
{
    public class RemoveResourceDomainEventHandler : IDomainEventHandler<RemoveResourceDomainEvent>
    {
        private readonly IResourceLogHistoryRepository _repository;
        public RemoveResourceDomainEventHandler(IResourceLogHistoryRepository repository)
        {
            _repository = repository;
        }
        public async Task Handle(RemoveResourceDomainEvent @event)
        {
            var resource = await _repository.GetbyResourceId(@event.ResourceId);
            if(resource != null)
            {
                await _repository.Remove(resource);
            }
        }
    }
}
