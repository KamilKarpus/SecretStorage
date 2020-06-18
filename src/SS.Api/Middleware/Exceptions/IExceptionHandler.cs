using System;

namespace SS.Api.Middleware.Exceptions
{
    public interface IExceptionHandler
    {
        ReponseDetails HandleException(Exception exception);
    }
}
