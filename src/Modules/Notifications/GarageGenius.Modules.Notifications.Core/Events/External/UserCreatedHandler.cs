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
        _logger.Information("Handled UserCreated event by {ModuleName} module and send notification to email: {Email}", nameof(Notifications), @event.Email);
        return Task.CompletedTask;
    }
}