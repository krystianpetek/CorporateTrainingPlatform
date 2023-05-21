using GarageGenius.Modules.Customers.Core.Entities;
using GarageGenius.Modules.Customers.Core.Repositories;
using GarageGenius.Modules.Users.Shared.Events;
using GarageGenius.Shared.Abstractions.Events;

namespace GarageGenius.Modules.Customers.Application.Events.External;
internal sealed class UserCreatedHandler : IEventHandler<UserCreated>
{
    private readonly Serilog.ILogger _logger;
    private readonly ICustomerRepository _customerRepository;

    public UserCreatedHandler(Serilog.ILogger logger, ICustomerRepository customerRepository)
    {
        _logger = logger;
        _customerRepository = customerRepository;
    }

    public async Task HandleEventAsync(UserCreated @event, CancellationToken cancellationToken = default)
    {
        await _customerRepository.AddCustomerAsync(new Customer(@event.UserId, @event.Email), cancellationToken);
        _logger.Information(
            messageTemplate: "Event {EventName} handled by {ModuleName} module, added customer with user ID: {UserId}",
            nameof(UserCreated),
            nameof(Customers),
            @event.UserId);
    }
}