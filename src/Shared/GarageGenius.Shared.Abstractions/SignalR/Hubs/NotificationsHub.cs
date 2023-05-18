using Microsoft.AspNetCore.SignalR;

namespace GarageGenius.Shared.Abstractions.SignalR.Hubs;
public class NotificationsHub : Hub
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

    public async Task PackageAsync(string data)
    {
        _logger.Information("Package received: {data}", data);
        await Clients.All.SendAsync("packageasync", data);
    }
}
