using SS.Common.Exceptions;

namespace SS.Users.Infrastructure.UserSecurity.Passwords.Exception
{
    public class PasswordException : SSException
    {
        public PasswordException(string message) : base(message)
        {
        }

        public override string ExceptionMessage => Message;

        public override uint ErrorCode => 401;

        public override uint InternalErrorCode => throw new System.NotImplementedException();
    }
}
