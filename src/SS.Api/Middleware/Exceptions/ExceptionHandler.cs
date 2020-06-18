using SS.Common.Exceptions;
using System;
using System.Net;

namespace SS.Api.Middleware.Exceptions
{
    public class ExceptionHandler : IExceptionHandler
    {
        public ReponseDetails HandleException(Exception exception)
        {
            if(exception is SSException)
            {
                var ss = exception as SSException;
                return new ReponseDetails()
                {
                    ErrorCode = ss.InternalErrorCode,
                    ErrorMessage = ss.Message,
                    StatusCode = (int)ss.ErrorCode
                };
            }
            return new ReponseDetails()
            {
                ErrorCode = (int)HttpStatusCode.InternalServerError,
                ErrorMessage = exception.Message,
                StatusCode = (int)HttpStatusCode.InternalServerError,
            };
        }
    }
}
