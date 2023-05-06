using GarageGenius.Shared.Abstractions.Events;

namespace GarageGenius.Modules.Users.Core.Events;
public record UserCreated(Guid UserId, string Email) : IEvent;