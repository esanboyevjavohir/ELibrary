using ELibrary.Business.Exceptions;
using ELibrary.Core.Common;

namespace ELibrary.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
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
            catch (ELibraryException ex)
            {
                _logger.LogError(ex, "Business error: {Code} - {Message}",
                    ex.Error.code, ex.Error.message);

                await HandleExceptionAsync(context, ex.Error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error: {Message}", ex.Message);

                await HandleExceptionAsync(context, Errors.InternalServerError);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Errors error)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = error.code switch
            {
                "Error.NotFound" => StatusCodes.Status404NotFound,
                "Error.Unauthorized" => StatusCodes.Status401Unauthorized,
                "Error.Forbidden" => StatusCodes.Status403Forbidden,
                "Error.Conflict" => StatusCodes.Status409Conflict,
                "Error.LoginFailed" => StatusCodes.Status401Unauthorized,
                "Error.InsufficientBalance" => StatusCodes.Status400BadRequest,
                "Error.NoCopiesAvailable" => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            await context.Response.WriteAsJsonAsync(new
            {
                error.code,
                error.message
            });
        }
    }
}
