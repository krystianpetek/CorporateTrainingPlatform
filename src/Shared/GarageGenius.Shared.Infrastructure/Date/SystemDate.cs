using GarageGenius.Shared.Abstractions.Date;
using Microsoft.Extensions.DependencyInjection;

namespace GarageGenius.Shared.Infrastructure.Date;
internal class SystemDate : ISystemDate
{
    public DateTime GetCurrentDate()
    {
        return DateTime.UtcNow;
    }
}

public static class Extensions
{
    public static IServiceCollection AddSystemDate(this IServiceCollection services)
    {
        services.AddSingleton<ISystemDate, SystemDate>();
        return services;
    }
}
