using GarageGenius.Shared.Abstractions.Events;

namespace GarageGenius.Modules.Users.Core.Events;
public sealed class UserCreatedHandler : IEventHandler<UserCreated>
{
    public UserCreatedHandler()
    {
    }

    public async Task HandleAsync(UserCreated @event, CancellationToken cancellationToken = default) =>
        await Console.Out.WriteLineAsync("WIP but works");
}