using GarageGenius.Modules.Users.Shared.Events;
using GarageGenius.Shared.Abstractions.Events;
using GarageGenius.Shared.Infrastructure.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace GarageGenius.Modules.Notifications.Core.Events.External;
internal sealed class UserCreatedHandler : IEventHandler<UserCreated>
{
    private readonly Serilog.ILogger _logger;
    private readonly IHubContext<NotificationsHub> _hubContextNotifications;

    public UserCreatedHandler(
        Serilog.ILogger logger,
        IHubContext<NotificationsHub> hubContextNotifications)
    {
        _logger = logger;
        _hubContextNotifications = hubContextNotifications;
    }

    public async Task HandleEventAsync(UserCreated @event, CancellationToken cancellationToken = default)
    {
        await _hubContextNotifications.Clients.All.SendAsync("SendNotification", DateTime.Now, @event.Email);

        _logger.Information(
            messageTemplate: "Event {EventName} handled by {ModuleName} module, added customer with user ID: {UserId}",
            nameof(UserCreated),
            nameof(Notifications),
            @event.UserId);
        // TODO: Send email notification to user ?
    }
}