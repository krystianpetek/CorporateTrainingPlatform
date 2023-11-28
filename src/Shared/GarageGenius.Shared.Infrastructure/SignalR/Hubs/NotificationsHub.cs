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

	public override Task OnConnectedAsync()
	{
		_logger.Information("Connected with SignalR NotificationsHub");
        return base.OnConnectedAsync();
    }

	public async Task JoinToHub(string groupName)
	{
		await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
		_logger.Information("Joining to group: {groupName}", groupName);
	}
	
	public Task SendMessage(string user, string message)
	{
		return Clients.All.SendMessage(user, message);
	}

	public Task PackageAsync(NotificationsHubModel notificationsHubModel)
	{
		_logger.Information("Package received: {data}", notificationsHubModel);
		//return Clients.All.SendAsync("ReceiveMessage", notificationsHubModel);
        return Clients.Caller.SendMessageAsync(notificationsHubModel);
    }
}
