using SS.Common.Exceptions;

namespace SS.Common.BuldingBlocks
{
    public interface IBusinessRule
    {
        bool isBroken();
        SSException Exception { get; }
    }
}
