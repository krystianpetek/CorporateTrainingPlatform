using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;

namespace GarageGenius.Shared.Infrastructure.Middleware.ErrorHandling;
public class ErrorHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, exception.Message);
            await HandleExceptionAsync(context, exception);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        HttpStatusCode statusCode;
        var stackTrace = exception.StackTrace;
        var message = exception.Message;
        switch (exception)
        {
            //case BadRequestException:
            //    statusCode = HttpStatusCode.BadRequest;
            //    break;
            //case NotFoundException:
            //    statusCode = HttpStatusCode.NotFound;
            //    break;
            case NotImplementedException:
                statusCode = HttpStatusCode.NotImplemented;
                break;
            case UnauthorizedAccessException:
                statusCode = HttpStatusCode.Unauthorized;
                break;
            case KeyNotFoundException:
                statusCode = HttpStatusCode.Unauthorized;
                break;
            default:
                message = exception.Message;
                statusCode = HttpStatusCode.InternalServerError;
                break;
        }

        var exceptionResult = JsonSerializer.Serialize(new { error = message /*, stackTrace */ });
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;
        return context.Response.WriteAsync(exceptionResult);
    }
}

public static class Extensions
{
    public static IServiceCollection AddErrorHandling(this IServiceCollection services)
    {
        return services.AddScoped<ErrorHandlingMiddleware>();
    }

    public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ErrorHandlingMiddleware>();
    }
}
