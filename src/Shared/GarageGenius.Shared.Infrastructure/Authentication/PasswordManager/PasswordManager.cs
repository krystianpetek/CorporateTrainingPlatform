using GarageGenius.Shared.Abstractions.Authentication.PasswordManager;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace GarageGenius.Shared.Infrastructure.Authentication.PasswordManager;
internal class PasswordManager : IPasswordManager
{
    private readonly IPasswordHasher<object> _passwordHasher;

    public PasswordManager(IPasswordHasher<object> passwordHasher)
    {
        _passwordHasher = passwordHasher;
    }

    public string Generate(string password)
    {
        return _passwordHasher.HashPassword(new object(), password);
    }

    public bool IsValid(string password, string securedPassword)
    {
        PasswordVerificationResult result = _passwordHasher.VerifyHashedPassword(new object(), securedPassword, password);
        return result == PasswordVerificationResult.Success;
    }
}

public static class Extensions
{
    public static IServiceCollection AddPasswordManager(this IServiceCollection services)
    {
        services.AddSingleton<IPasswordHasher<object>, PasswordHasher<object>>();
        services.AddSingleton<IPasswordManager, PasswordManager>();
        return services;
    }
}
