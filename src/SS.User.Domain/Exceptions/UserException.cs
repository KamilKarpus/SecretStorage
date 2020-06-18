using SS.Common.Exceptions;

namespace SS.Users.Domain.Exceptions
{
    public class UserException : SSException
    {
        private readonly HttpCodes _code;
        private readonly ErrorCodes _internalCode;
        private readonly string _message;
        public UserException(string message, HttpCodes code, ErrorCodes internalCode) : base(message)
        {
            _code = code;
            _message = message;
            _internalCode = internalCode;
        }

        public override string ExceptionMessage => _message;

        public override uint ErrorCode => (uint)_code;

        public override uint InternalErrorCode => (uint)_internalCode;
    }
}
