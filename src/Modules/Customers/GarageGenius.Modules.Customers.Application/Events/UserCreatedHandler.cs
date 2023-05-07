using GarageGenius.Modules.Users.Shared.Events;
using GarageGenius.Shared.Abstractions.Events;

namespace GarageGenius.Modules.Customers.Application.Events;
public sealed class UserCreatedHandler : IEventHandler<UserCreated>
{
    public UserCreatedHandler()
    {
    }

    public async Task HandleAsync(UserCreated @event, CancellationToken cancellationToken = default) =>
        await Console.Out.WriteLineAsync("WIP but works");
}