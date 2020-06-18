using System.Threading.Tasks;

namespace SS.Infrastructure.EventBus
{
    public interface IIntegrationEventHandler<in T> : IIntegrationEventHandler where T : IIntegrationEvent
    {
        Task Handle(T @event);
    }

    public interface IIntegrationEventHandler
    {
    }

}