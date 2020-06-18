using SS.Common.Exceptions;

namespace SS.Rijndael.Cryptography.Exceptions
{
    public class IVException : SSException
    {
        private HttpCodes _code;
        private ExceptionCode _exceptionCode;
        private string _message;
        public IVException(HttpCodes code, ExceptionCode exceptionCode, string message) : base(message)
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
