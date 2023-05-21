using GarageGenius.Shared.Abstractions.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace GarageGenius.Shared.Infrastructure.SignalR.Hubs;
public class NotificationsHub : Hub<INotificationsHub>
{
    private readonly Serilog.ILogger _logger;

    public NotificationsHub(Serilog.ILogger logger)
    {
        _logger = logger;
    }

    public override async Task OnConnectedAsync()
    {
        _logger.Information("Connected with SignalR NotificationsHub");
        await base.OnConnectedAsync();
    }

    public async Task PackageAsync(NotificationsHubModel notificationsHubModel)
    {
        _logger.Information("Package received: {data}", notificationsHubModel);
        await Clients.Caller.SendMessageAsync(notificationsHubModel);
    }
}
