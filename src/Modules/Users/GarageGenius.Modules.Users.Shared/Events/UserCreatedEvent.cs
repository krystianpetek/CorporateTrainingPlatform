using GarageGenius.Shared.Abstractions.Events;

namespace GarageGenius.Modules.Users.Shared.Events;
public record UserCreatedEvent(Guid UserId, Guid CustomerId, string EmailAddress) : IEvent;