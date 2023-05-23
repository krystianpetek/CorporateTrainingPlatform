using GarageGenius.Modules.Users.Shared.Events;
using GarageGenius.Shared.Abstractions.Events;

namespace GarageGenius.Modules.Notifications.Core.Events.External;
internal sealed class UserCreatedHandler : IEventHandler<UserCreated>
{
    private readonly Serilog.ILogger _logger;

    public UserCreatedHandler(Serilog.ILogger logger)
    {
        _logger = logger;
    }

    public Task HandleEventAsync(UserCreated @event, CancellationToken cancellationToken = default)
    {
        // TODO: Send email notification to user
        _logger.Information(
            messageTemplate: "Event {EventName} handled by {ModuleName} module, added customer with user ID: {UserId}",
            nameof(UserCreated),
            nameof(Notifications),
            @event.UserId);
        return Task.CompletedTask;
    }
}