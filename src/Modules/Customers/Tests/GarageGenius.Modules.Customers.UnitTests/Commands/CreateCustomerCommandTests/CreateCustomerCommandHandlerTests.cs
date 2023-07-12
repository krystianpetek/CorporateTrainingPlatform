using GarageGenius.Modules.Customers.Application.Commands.CreateCustomer;
using GarageGenius.Modules.Customers.Application.MapperService;
using GarageGenius.Modules.Customers.Core.Entities;
using GarageGenius.Modules.Customers.Core.Repositories;
using GarageGenius.Shared.Abstractions.Commands;
using Moq;
using Xunit;

namespace GarageGenius.Modules.Customers.UnitTests.Commands.CreateCustomerCommandTests;
public class CreateCustomerCommandHandlerTests
{
    private readonly Mock<ICustomerRepository> _customerRepositoryMock = new Mock<ICustomerRepository>();
    private readonly Mock<ICustomerMapperService> _customerMapperServiceMock = new Mock<ICustomerMapperService>();
    private readonly Mock<Serilog.ILogger> _loggerMock = new Mock<Serilog.ILogger>();
    private ICommandHandler<CreateCustomerCommand> _handler;

    [Fact]
    public async Task HandleCommand_CreateCustomer_Should_CreateNewCustomer()
    {
        // arrange
        CreateCustomerCommand command = new CreateCustomerCommand
        {
            FirstName = "Krystian",
            LastName = "Petek",
            EmailAddress = "krystianpetek2@gmail.com",
            PhoneNumber = "123123123"
        };
        Customer customer = new Customer(command.FirstName, command.LastName, command.PhoneNumber, command.EmailAddress);
        _customerMapperServiceMock.Setup(x => x.MapToCustomer(command)).Returns(customer);
        _customerRepositoryMock.Setup(x => x.GetCustomerByIdAsync(customer.CustomerId.Value, new CancellationToken(false))).ReturnsAsync(customer);
        _handler = new CreateCustomerCommandHandler(_loggerMock.Object, _customerRepositoryMock.Object, _customerMapperServiceMock.Object);

        // act
        await _handler.HandleCommandAsync(command);

        // assert
        _customerMapperServiceMock.Verify(x => x.MapToCustomer(It.IsAny<CreateCustomerCommand>()), Times.Once);
        _customerRepositoryMock.Verify(x => x.AddCustomerAsync(It.IsAny<Customer>(), new CancellationToken()), Times.Once);
    }
}
