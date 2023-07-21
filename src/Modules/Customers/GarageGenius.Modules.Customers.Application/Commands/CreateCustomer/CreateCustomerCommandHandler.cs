using GarageGenius.Modules.Customers.Application.MapperService;
using GarageGenius.Modules.Customers.Core.Entities;
using GarageGenius.Modules.Customers.Core.Repositories;
using GarageGenius.Shared.Abstractions.Commands;

namespace GarageGenius.Modules.Customers.Application.Commands.CreateCustomer;
internal class CreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand>
{
	private readonly Serilog.ILogger _logger;
	private readonly ICustomerRepository _customerRepository;
	private readonly ICustomerMapperService _customerMapperService;

	public CreateCustomerCommandHandler(
		Serilog.ILogger logger,
		ICustomerRepository customerRepository,
		ICustomerMapperService customerMapperService)
	{
		_logger = logger;
		_customerRepository = customerRepository;
		_customerMapperService = customerMapperService;
	}

	public async Task HandleCommandAsync(CreateCustomerCommand command, CancellationToken cancellationToken = default)
	{
		Customer customer = _customerMapperService.MapToCustomer(command);
		await _customerRepository.AddCustomerAsync(customer, cancellationToken);

		// TODO - validate if customer email exists

		_logger.Information("Handled {CommandName} in {ModuleName} module, created customer with email: {Email}", nameof(CreateCustomerCommand), nameof(Customers), command.EmailAddress);
	}
}
