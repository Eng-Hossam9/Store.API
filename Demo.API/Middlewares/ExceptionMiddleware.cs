using Demo.API.Errors;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Demo.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _environment;

        public ExceptionMiddleware(RequestDelegate next,ILogger<ExceptionMiddleware> logger,IHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext context) 
        {
            try
            {
               await _next.Invoke(context);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex,ex.Message);
                context.Response.ContentType = "application/json"; 
                context.Response.StatusCode=StatusCodes.Status500InternalServerError;
                var exception = _environment.IsDevelopment() ? new ApiExceptionServer(StatusCodes.Status500InternalServerError, ex.Message, ex.StackTrace?.ToString() )
                    : new ApiExceptionServer(StatusCodes.Status500InternalServerError);

                var option = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                };


                var Response = JsonSerializer.Serialize(exception, option);
               await context.Response.WriteAsync(Response);
            }
        }
    }
}
