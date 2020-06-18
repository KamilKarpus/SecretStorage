using System;
using System.Collections.Generic;
using System.Text;

namespace SS.Common.Exceptions
{
    public abstract class SSException : Exception
    {
        public abstract string ExceptionMessage { get; }
        public abstract uint ErrorCode { get; }
        public abstract uint InternalErrorCode { get; }
        public SSException(string message) : base(message)
        {
        }
    }
}
