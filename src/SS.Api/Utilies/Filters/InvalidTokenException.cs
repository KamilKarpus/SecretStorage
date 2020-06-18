using SS.Common.Exceptions;


namespace SS.Api.Utilies.Filters
{
    public class InvalidTokenException : SSException
    {
        public override string ExceptionMessage => _message;

        public override uint ErrorCode => _code;

        public override uint InternalErrorCode => _internalCode;

        private uint _code;
        private string _message;
        private uint _internalCode;
        public InvalidTokenException(string message, uint errorCode, uint internalCode) : base(message)
        {
            _code = errorCode;
            _message = message;
            _internalCode = internalCode;
        }
    }
}
