﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GarageGenius.Shared.Infrastructure.Persistance.SqlServer;
public static class Extensions
{
    public static IServiceCollection AddSqlServerDbContext<T>(this IServiceCollection services) where T : DbContext
    {
        using ServiceProvider? serviceProvider = services.BuildServiceProvider();
        IConfiguration configuration = serviceProvider.GetService<IConfiguration>();
        SqlServerConnection connection = configuration.GetRequiredSection("ConnectionStrings").GetRequiredSection("SqlServerConnection").Get<SqlServerConnection>();

        services.AddDbContext<T>(x => x.UseSqlServer(connection.ConnectionString));

        return services;
    }
}
