using GarageGenius.Modules.Customers.Application.MapperService;
using GarageGenius.Modules.Customers.Core.Entities;
using GarageGenius.Modules.Customers.Core.Repositories;
using GarageGenius.Modules.Users.Shared.Events;
using GarageGenius.Shared.Abstractions.Events;

namespace GarageGenius.Modules.Customers.Application.Events.External;
internal sealed class UserCreatedEventHandler : IEventHandler<UserCreatedEvent>
{
	private readonly Serilog.ILogger _logger;
	private readonly ICustomerRepository _customerRepository;
	private readonly ICustomerMapperService _customerMapperService;

	public UserCreatedEventHandler(
		Serilog.ILogger logger,
		ICustomerRepository customerRepository,
		ICustomerMapperService customerMapperService)
	{
		_logger = logger;
		_customerRepository = customerRepository;
		_customerMapperService = customerMapperService;
	}

	public async Task HandleEventAsync(UserCreatedEvent @event, CancellationToken cancellationToken = default)
	{
		_customerMapperService.MapToCustomer(@event);
		await _customerRepository.AddCustomerAsync(new Customer(@event.CustomerId, @event.UserId, @event.EmailAddress), cancellationToken);
		_logger.Information(
			messageTemplate: "Event {EventName} handled by {ModuleName} module, added customer with user ID: {UserId}",
			nameof(UserCreatedEvent),
			nameof(Customers),
			@event.UserId);
	}
}