using System.Net;
using System.Text.Json;

namespace GarageGenius.WebApi.Middlewares.ErrorHandling;
public class GlobalErrorHandlingMiddleware : IMiddleware
{
	private readonly Serilog.ILogger _logger;

	public GlobalErrorHandlingMiddleware(Serilog.ILogger logger)
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
			_logger.Error(exception, exception.Message);
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
	public static IServiceCollection AddGlobalErrorHandling(this IServiceCollection services)
	{
		return services.AddScoped<GlobalErrorHandlingMiddleware>();
	}

	public static IApplicationBuilder UseGlobalErrorHandling(this IApplicationBuilder app)
	{
		return app.UseMiddleware<GlobalErrorHandlingMiddleware>();
	}
}
