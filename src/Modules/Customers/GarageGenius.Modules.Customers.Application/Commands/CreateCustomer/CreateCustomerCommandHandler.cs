using GarageGenius.Modules.Customers.Core.Entities;
using GarageGenius.Modules.Customers.Core.Repositories;
using GarageGenius.Shared.Abstractions.Commands;

namespace GarageGenius.Modules.Customers.Application.Commands.CreateCustomer;
internal class CreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand>
{
    private readonly Serilog.ILogger _logger;
    private readonly ICustomerRepository _customerRepository;

    public CreateCustomerCommandHandler(
        Serilog.ILogger logger,
        ICustomerRepository customerRepository)
    {
        _logger = logger;
        _customerRepository = customerRepository;
    }

    public async Task HandleAsync(CreateCustomerCommand command, CancellationToken cancellationToken = default)
    {
        Customer customer = new Customer(command.FirstName, command.LastName, command.PhoneNumber, command.EmailAddress);
        await _customerRepository.AddAsync(customer);

        _logger.Information("Handled {CommandName} in {ModuleName} module, created customer with email: {Email}", nameof(CreateCustomerCommand), nameof(Customers), command.EmailAddress);
    }
}
