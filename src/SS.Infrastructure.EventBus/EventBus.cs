using System.Threading.Tasks;

namespace SS.Infrastructure.EventBus
{
    public sealed class InMemoryEventBusClient : IEventBus
    {
        public async Task PublishAsync<T>(T @event) where T : IIntegrationEvent
        {
            await InMemoryEventBus.Instance.Publish(@event);
        }

        public void Subscribe<T>(IIntegrationEventHandler<T> handler) where T : IIntegrationEvent
        {
            InMemoryEventBus.Instance.Subscribe(handler);
        }

    }
}
