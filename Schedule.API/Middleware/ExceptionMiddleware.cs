using System.Net;
using System.Text.Json;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Schedule.API.Middleware
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
            catch (Exception exception)
            {
                _logger.LogError(exception, "Unhandled exception");
                await WriteErrorAsync(context, exception);
            }
        }

        private static async Task WriteErrorAsync(HttpContext context, Exception exception)
        {
            var (status, message) = Map(exception);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = status;

            var body = new ErrorResponse
            {
                Status = status,
                Message = message
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(body));
        }

        private static (int Status, string Message) Map(Exception exception)
        {
            if (exception is DbUpdateException dbUpdate)
            {
                var sqlException = dbUpdate.InnerException as SqlException
                    ?? dbUpdate.GetBaseException() as SqlException;

                if (sqlException?.Number == 547)
                    return ((int)HttpStatusCode.Conflict, "The request conflicts with related data.");

                if (sqlException?.Number is 2601 or 2627)
                    return ((int)HttpStatusCode.Conflict, "A record with the same unique value already exists.");

                return ((int)HttpStatusCode.Conflict, "The data could not be saved because of a database constraint.");
            }

            return ((int)HttpStatusCode.InternalServerError, "An unexpected error occurred.");
        }
    }
}
