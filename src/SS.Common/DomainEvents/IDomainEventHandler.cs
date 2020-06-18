using SS.Common.BuldingBlocks;
using System.Threading.Tasks;

namespace SS.Common.DomainEvents
{
    public interface IDomainEventHandler<T> where T : IDomainEvent
    {
        Task Handle(T @event);
    }
}
