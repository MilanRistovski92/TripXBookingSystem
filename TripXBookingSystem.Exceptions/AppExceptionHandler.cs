using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;

namespace TripXBookingSystem.Exceptions
{
    public class AppExceptionHandler
    {
        private readonly RequestDelegate _next;

        public AppExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = JsonConvert.SerializeObject(new { error = "An unexpected error occurred. Please try again later." });
            var logger = context.RequestServices.GetRequiredService<ILogger<AppExceptionHandler>>();
            logger.LogError(exception, "Handled an unexpected error.");

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
