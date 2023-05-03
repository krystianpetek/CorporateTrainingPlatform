using GarageGenius.Shared.Abstractions.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace GarageGenius.Modules.Users.Core.Events;
public sealed class UserCreatedHandler : IEventHandler<UserCreated>
{
    public UserCreatedHandler()
    {
    }

    public async Task HandleAsync(UserCreated @event, CancellationToken cancellationToken = default) =>
        await Console.Out.WriteLineAsync("WIP but works");
}