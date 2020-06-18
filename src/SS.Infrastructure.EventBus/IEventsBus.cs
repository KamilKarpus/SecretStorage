using System.Threading.Tasks;

namespace SS.Infrastructure.EventBus
{
    public interface IEventBus
    {
        Task PublishAsync<T>(T @event) where T : IIntegrationEvent;
    
        void Subscribe<T>(IIntegrationEventHandler<T> handler) where T : IIntegrationEvent;
    
    }

}