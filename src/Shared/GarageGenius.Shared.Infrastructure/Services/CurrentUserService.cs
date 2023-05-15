using GarageGenius.Shared.Abstractions.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace GarageGenius.Shared.Infrastructure.Services;
internal class CurrentUserService : ICurrentUserService
{
    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        IsAuthenticated = UserId is not null;
        UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? "Anonymous";
        Role = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Role);
    }

    public string UserId { get; }

    public string Role { get; }

    public bool IsAuthenticated { get; }
}

public static partial class Extensions
{
    public static IServiceCollection AddSharedCurrentUser(this IServiceCollection services)
    {
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        return services;
    }
}