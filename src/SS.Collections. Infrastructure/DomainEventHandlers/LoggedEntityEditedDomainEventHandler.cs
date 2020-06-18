using SS.Collections.Domain.DomainEvents;
using SS.Collections.Domain.Repositories;
using SS.Common.DomainEvents;
using System.Threading.Tasks;

namespace SS.Collections._Infrastructure.DomainEventHandlers
{
    public class LoggedEntityEditedDomainEventHandler : IDomainEventHandler<LoggedEntityEditedDomainEvent>
    {
        private readonly IResourceLogHistoryRepository _repository;
        public LoggedEntityEditedDomainEventHandler(IResourceLogHistoryRepository repository)
        {
            _repository = repository;
        }
        public async Task Handle(LoggedEntityEditedDomainEvent @event)
        {
            var history = await _repository.GetbyResourceId(@event.ResourceId);
            history.ApplyEvent(@event);
            await _repository.UpdateAsync(history);
        }
    }
}
