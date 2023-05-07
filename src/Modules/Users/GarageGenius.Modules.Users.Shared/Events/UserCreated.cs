using GarageGenius.Shared.Abstractions.Events;

namespace GarageGenius.Modules.Users.Shared.Events;
public record UserCreated(Guid UserId, string Email) : IEvent;