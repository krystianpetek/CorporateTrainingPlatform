﻿using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("GarageGenius.Modules.Customers.Api")]
namespace GarageGenius.Modules.Customers.Application;

internal static class Extensions
{
    public static IServiceCollection AddCustomersApplication(this IServiceCollection services)
    {
        return services;
    }
}