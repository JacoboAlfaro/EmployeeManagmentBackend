using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.WebApi.Models;
using System.Text.Json;

namespace CleanArchitecture.WebApi.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }

            catch (Exception exception)
            {
                _logger.LogError(exception, "Ocurrió un error inesperado.");
                await HandleExceptionAsync(context, exception);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            int statusCode;
            string message;

            switch (exception)
            {
                case NotFoundException notFoundException:
                    statusCode = StatusCodes.Status404NotFound;
                    message = notFoundException.Message;
                    break;
                case BusinessException businessException:
                    statusCode = StatusCodes.Status400BadRequest;
                    message = businessException.Message;
                    break;
                default:
                    statusCode = StatusCodes.Status500InternalServerError;
                    message = "Un error inesperado ocurrió.";
                    break;
            }

            var response = new ErrorResponse(false, statusCode, message);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
