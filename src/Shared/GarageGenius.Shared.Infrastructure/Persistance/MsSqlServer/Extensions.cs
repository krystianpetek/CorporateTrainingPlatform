using GarageGenius.Shared.Infrastructure.Persistance.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GarageGenius.Shared.Infrastructure.Persistance.MsSqlServer;
public static class Extensions
{
    public static IServiceCollection AddMsSqlServerDbContext<T>(this IServiceCollection services) where T : DbContext
    {
        using ServiceProvider? serviceProvider = services.BuildServiceProvider();
        IConfiguration configuration = serviceProvider.GetService<IConfiguration>();
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();

        string connection = configuration.GetConnectionString("SqlServerConnection");
        bool inMemoryDatabase = configuration.GetValue<bool>("InMemoryDatabase");

        if (inMemoryDatabase)
            services.AddDbContext<T>(x => x.UseInMemoryDatabase(connection));
        else
            services.AddDbContext<T>(x => x.UseSqlServer(connection));

        return services;
    }
}
