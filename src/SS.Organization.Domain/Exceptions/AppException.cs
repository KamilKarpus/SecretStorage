using SS.Common.Exceptions;

namespace SS.Organizations.Domain.Exceptions
{
    public class AppException : SSException
    {
        private readonly HttpErrorCodes _code;
        private readonly string _message;
        private readonly InternalErrorCodes _internalCode;
        public AppException(string message, HttpErrorCodes code, InternalErrorCodes internalCode) : base(message)
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
