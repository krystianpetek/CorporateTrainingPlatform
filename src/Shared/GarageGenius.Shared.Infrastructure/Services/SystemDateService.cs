using GarageGenius.Shared.Abstractions.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GarageGenius.Shared.Infrastructure.Services;
internal class SystemDateService : ISystemDateService
{
	public DateTime GetCurrentDate()
	{
		return DateTime.UtcNow;
	}
}

public static partial class Extensions
{
	public static IServiceCollection AddSharedSystemDate(this IServiceCollection services)
	{
		services.AddSingleton<ISystemDateService, SystemDateService>();
		return services;
	}
}
