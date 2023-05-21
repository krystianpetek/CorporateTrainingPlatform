using GarageGenius.Modules.Customers.Core.Entities;
using GarageGenius.Modules.Customers.Core.Exceptions;
using GarageGenius.Modules.Customers.Core.Repositories;
using GarageGenius.Shared.Abstractions.Commands;

namespace GarageGenius.Modules.Customers.Application.Commands.UpdateCustomer;
internal class UpdateCustomerCommandHandler : ICommandHandler<UpdateCustomerCommand>
{
    private readonly Serilog.ILogger _logger;
    private readonly ICustomerRepository _customerRepository;

    public UpdateCustomerCommandHandler(
        Serilog.ILogger logger,
        ICustomerRepository customerRepository)
    {
        _logger = logger;
        _customerRepository = customerRepository;
    }

    public async Task HandleCommandAsync(UpdateCustomerCommand command, CancellationToken cancellationToken = default)
    {
        Customer? customer = await _customerRepository.GetCustomerByIdAsync(command.Id) ?? throw new CustomerNotFoundException(command.Id);
        customer.Update(command.FirstName, command.LastName, command.PhoneNumber);
        await _customerRepository.UpdateCustomerAsync(customer, cancellationToken);

        _logger.Information("Handled {CommandName} in {ModuleName} module, update customer with ID: {CustomerId}", nameof(UpdateCustomerCommand), nameof(Customers), command.Id);
    }
}
