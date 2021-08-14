using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Recipes.Application.Exceptions;

namespace Recipes.WebApi.ExceptionHandling
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
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
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            var message = "При обработке запроса произошла неизвестная ошибка.";
            
            switch (exception)
            {
                case UserRegistrationException userRegistrationException:
                {
                    statusCode = HttpStatusCode.Conflict;
                    message = userRegistrationException.Value;
                    break;
                }
                case UserLoginException userLoginException:
                {
                    statusCode = HttpStatusCode.Unauthorized;
                    message = userLoginException.Value;
                    break;
                }
                case ResourceNotFoundException resourceNotFoundException:
                {
                    statusCode = HttpStatusCode.NotFound;
                    message = resourceNotFoundException.Value;
                    break;
                }
                case PermissionException permissionException:
                {
                    statusCode = HttpStatusCode.Forbidden;
                    message = permissionException.Value;
                    break;
                }
                default:
                {
                    _logger.LogError("An unhandled exception has reached the middleware:\r\n" + exception);
                    break;
                }
            }
            
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            
            await context.Response.WriteAsync(new ErrorDetails
            {
                Status = context.Response.StatusCode,
                Message = message
            }.ToString());
        }
    }
}