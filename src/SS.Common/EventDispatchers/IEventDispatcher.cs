using SS.Common.BuldingBlocks;
using System.Threading.Tasks;

namespace SS.Common.EventDispatchers
{
    public interface IEventDispatcher
    {
        Task Dispatch(Entity entity);
    }
}