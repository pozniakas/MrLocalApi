using Microsoft.AspNetCore.Http;
using MrLocal_Backend.Controllers.LoggerService.Interfaces;
using MrLocal_Backend.Models;
using System;
using System.Net;
using System.Threading.Tasks;

namespace MrLocal_Backend.Controllers.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerManager _logger;

        public ExceptionMiddleware(RequestDelegate next, ILoggerManager logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            if (exception.GetType().IsAssignableFrom(typeof(ArgumentException)))
            {
                _logger.LogWarn($"User-friendly error: {exception}");
                return context.Response.WriteAsync(new ErrorDetails()
                {
                    StatusCode = context.Response.StatusCode = 400,
                    Message = exception.Message
                }.ToString());
            }
            else
            {
                _logger.LogError($"Internal server error: {exception}");
                return context.Response.WriteAsync(new ErrorDetails()
                {
                    StatusCode = context.Response.StatusCode,
                    Message = "Internal Server Error"
                }.ToString());
            }


        }
    }
}
