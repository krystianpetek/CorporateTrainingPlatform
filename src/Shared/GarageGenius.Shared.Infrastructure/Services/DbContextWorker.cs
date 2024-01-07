using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GarageGenius.Shared.Infrastructure.Services;

public class DbContextWorker : BackgroundService
{
	private readonly IServiceProvider _serviceProvider;

	public DbContextWorker(IServiceProvider serviceProvider)
	{
		_serviceProvider = serviceProvider;
	}

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var dbContextTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(x => x.GetTypes())
            .Where(x => typeof(DbContext).IsAssignableFrom(x) && !x.IsInterface && x != typeof(DbContext));

        //https://stackoverflow.com/questions/77575760/could-not-load-type-sqlguidcaster-from-assembly-microsoft-data-sqlclient-ver - NET 8 fix

        using var scope = _serviceProvider.CreateScope();
        foreach (var dbContextType in dbContextTypes)
        {
            var dbContext = scope.ServiceProvider.GetService(dbContextType) as DbContext;
            if (dbContext is null)
                continue;

            if (dbContext.Database.IsRelational())
            {
                await dbContext.Database.EnsureCreatedAsync(stoppingToken);
                await dbContext.Database.MigrateAsync(stoppingToken).ConfigureAwait(false);
            }

        }
    }
}