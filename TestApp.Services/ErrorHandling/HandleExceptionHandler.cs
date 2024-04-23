using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace TestApp.Services.ErrorHandling
{
    /// <summary>
    /// Exception handler.
    /// </summary>
    public static class HandleExceptionHandler
    {
        /// <summary>
        /// Error handler.
        /// </summary>
        /// <param name="context">HTTP context.</param>
        /// <returns>Task.</returns>
        public static async Task HandleExceptionAsync(HttpContext context)
        {
            var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

            if (exceptionHandlerPathFeature == null)
            {
                return;
            }

            var exception = exceptionHandlerPathFeature.Error;

            if (exception != null)
            {
                var loggerService = context.RequestServices.GetService<ILogger>();

                loggerService?.LogError(exception, "Unhandled exception");

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 500;

                await context.Response.WriteAsJsonAsync(new
                {
                    error = "An unexpected error occurred. Please try again later."
                }.ToString());
            }
        }
    }
}
