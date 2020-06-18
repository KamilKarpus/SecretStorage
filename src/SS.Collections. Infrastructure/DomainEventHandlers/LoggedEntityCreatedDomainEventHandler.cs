using SS.Collections._Infrastructure.Services;
using SS.Collections.Domain.DomainEvents;
using SS.Collections.Domain.History;
using SS.Collections.Domain.Repositories;
using SS.Common.DomainEvents;
using System.Threading.Tasks;

namespace SS.Collections._Infrastructure.DomainEventHandlers
{
    public class LoggedEntityCreatedDomainEventHandler : IDomainEventHandler<LoggedEntityCreatedDomainEvent>
    {
        private readonly IResourceLogHistoryRepository _repository;
        public LoggedEntityCreatedDomainEventHandler(IResourceLogHistoryRepository repository)
        {
            _repository = repository;
        }
        public async Task Handle(LoggedEntityCreatedDomainEvent @event)
        {
            var history = await _repository.GetbyResourceId(@event.ResourceId);
            if (history == null)
            {
                var logs = ResourceLogHistory.Create();
                logs.ApplyEvent(@event);
                await _repository.AddAsync(logs);
            }
    }
    }
}
