using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace TestApp.Services.Middleware;

public class AuditMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public AuditMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
    {
        _next = next;
        _logger = loggerFactory.CreateLogger("AuditMiddleware");
    }

    public async Task Invoke(HttpContext context)
    {
        // Log the request path
        _logger.LogInformation("Handling request: " + context.Request.Path);

        await _next(context);

        // Log the response status code
        _logger.LogInformation("Response status code: " + context.Response.StatusCode);
    }
}