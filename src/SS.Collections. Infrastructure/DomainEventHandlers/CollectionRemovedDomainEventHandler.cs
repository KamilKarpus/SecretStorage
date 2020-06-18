using SS.Collections.Domain.DomainEvents;
using SS.Collections.Domain.Repositories;
using SS.Common.DomainEvents;
using System.Threading.Tasks;

namespace SS.Collections._Infrastructure.DomainEventHandlers
{
    public class CollectionRemovedDomainEventHandler : IDomainEventHandler<CollectionRemovedDomainEvent>
    {

        private readonly IResourceLogHistoryRepository _repository;
        public CollectionRemovedDomainEventHandler(IResourceLogHistoryRepository repository)
        {
            _repository = repository;
        }
        public async Task Handle(CollectionRemovedDomainEvent @event)
        {
           await  _repository.RemoveManybyCollectionId(@event.CollectionId);
        }
    }
}
