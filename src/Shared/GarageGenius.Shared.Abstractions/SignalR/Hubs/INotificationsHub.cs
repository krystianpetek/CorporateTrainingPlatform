namespace GarageGenius.Shared.Abstractions.SignalR.Hubs;
public interface INotificationsHub
{
    Task SendMessageAsync(NotificationsHubModel message);
}
