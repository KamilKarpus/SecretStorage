using Microsoft.AspNetCore.Builder;
using SS.Api.Middleware.Exceptions;

namespace SS.Api.Middleware
{
    public static class Extensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
