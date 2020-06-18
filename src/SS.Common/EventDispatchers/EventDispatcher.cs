using Autofac;
using SS.Common.BuldingBlocks;
using SS.Common.DomainEvents;
using System.Threading.Tasks;

namespace SS.Common.EventDispatchers
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly ILifetimeScope _scope;
        public EventDispatcher(ILifetimeScope scope)
        {
            _scope = scope;
        }
        public async Task Dispatch(Entity entity)
        {
            var handlersTypes = typeof(IDomainEventHandler<>);
            foreach (var @event in entity.DomainEvents)
            {
                var genericType = handlersTypes.MakeGenericType(@event.GetType());
                dynamic handler = _scope.Resolve(genericType);
                await handler.Handle((dynamic)@event);

            }
            entity.ClearEvents();
        }
    }
}
