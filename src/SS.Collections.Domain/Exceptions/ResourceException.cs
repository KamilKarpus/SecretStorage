using SS.Common.Exceptions;

namespace SS.Collections.Domain.Exceptions
{
    public class ResourceException : SSException
    {
        private readonly string _message;
        private readonly HttpCodes _code;
        private readonly ExceptionCode _exceptionCode;
        public ResourceException(string message, HttpCodes code, ExceptionCode exceptionCode) : base(message)
        {
            _code = code;
            _message = message;
            _exceptionCode = exceptionCode;
        }

        public override string ExceptionMessage => _message;

        public override uint ErrorCode => (uint)_code;

        public override uint InternalErrorCode => (uint)_exceptionCode;
    }
}
