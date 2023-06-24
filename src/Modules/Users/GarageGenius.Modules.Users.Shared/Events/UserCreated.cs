using GarageGenius.Shared.Abstractions.Events;

namespace GarageGenius.Modules.Users.Shared.Events;
public record UserCreated(Guid UserId, Guid CustomerId, string Email) : IEvent;