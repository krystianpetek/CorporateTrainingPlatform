using FluentValidation;
using GarageGenius.Modules.Users.Application.Commands.CreateUser;
using GarageGenius.Modules.Users.Application.Commands.DeactivateUser;
using GarageGenius.Modules.Users.Application.Commands.SignIn;
using GarageGenius.Modules.Users.Application.Commands.SignUp;
using GarageGenius.Modules.Users.Application.Commands.SignUp.Policy;
using GarageGenius.Modules.Users.Application.Queries.GetUsers.Policy;
using GarageGenius.Modules.Users.Application.ServiceMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace GarageGenius.Modules.Users.Application;

internal static class Extensions
{
    public static IServiceCollection AddUsersApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthorizationHandler, SignUpPolicyHandler>();
        services.AddScoped<IAuthorizationHandler, GetUsersPolicyHandler>();

        services.AddAuthorization(authorizationOptions =>
        {
            authorizationOptions.SignUpPolicy();
            authorizationOptions.GetUsersPolicy();
        });
        services.AddScoped<IValidator<DeactivateUserCommand>, DeactivateUserCommandValidator>();
        services.AddScoped<IValidator<SignInCommand>, SignInCommandValidator>();
        services.AddScoped<IValidator<SignUpCommand>, SignUpCommandValidator>();
        services.AddScoped<IValidator<CreateUserCommand>, CreateUserCommandValidator>();

        services.AddScoped<IUserServiceMapper, UserServiceMapper>();

        
        return services;
    }
}