namespace GarageGenius.Shared.Abstractions.SignalR.Hubs;
public interface INotificationsHub
{
	Task SendMessageAsync(NotificationsHubModel message);
	Task SendMessage(string user, string message);
}
