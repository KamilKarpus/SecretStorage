using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace SS.Api.Middleware.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private IExceptionHandler _handler;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context, IExceptionHandler handler)
        {
            _handler = handler;
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                await HandleException(context, ex);
            }
        }
        public async Task HandleException(HttpContext context, Exception exception)
        {
            var response = _handler.HandleException(exception);
            context.Response.StatusCode = response.StatusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(new 
            {
               errorCode = response.ErrorCode,
               message = response.ErrorMessage,
            }));
        }
    }
}
