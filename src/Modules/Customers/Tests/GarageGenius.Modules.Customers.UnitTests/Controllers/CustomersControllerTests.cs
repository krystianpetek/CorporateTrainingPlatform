using GarageGenius.Modules.Customers.Api.Controllers;
using GarageGenius.Modules.Customers.Application.Commands.CreateCustomer;
using GarageGenius.Modules.Customers.Application.Commands.UpdateCustomer;
using GarageGenius.Modules.Customers.Application.Queries.GetCustomerById;
using GarageGenius.Modules.Customers.Application.Queries.GetCustomerByUserId;
using GarageGenius.Shared.Abstractions.Commands;
using GarageGenius.Shared.Abstractions.Dispatcher;
using GarageGenius.Shared.Abstractions.Queries.Query;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace GarageGenius.Modules.Customers.UnitTests.Controllers;
public class CustomersControllerTests
{
    private readonly Mock<IDispatcher> _dispatcher;

    public CustomersControllerTests()
    {
        _dispatcher = new Mock<IDispatcher>();
    }

    [Theory]
    [InlineData("Krystian", "Petek", "123321123", "krystianpetek2@gmail.com")]
    public async Task CustomersController_UpdateCustomerAsync_Should_Return_Correct_StatusCode(string firstName, string lastName, string phoneNumber, string emailAddress)
    {
        // arrange
        Guid guid = Guid.NewGuid();
        _dispatcher.Setup(dispatcher => dispatcher.DispatchCommandAsync(It.IsAny<ICommand>(), It.IsAny<CancellationToken>()));

        // act
        CustomersController customersController = new CustomersController(_dispatcher.Object);
        ActionResult result = await customersController.UpdateCustomerAsync(new UpdateCustomerCommand
        {
            Id = guid,
            FirstName = firstName,
            LastName = lastName,
            PhoneNumber = phoneNumber,
        });

        // assert
        Assert.IsType<NoContentResult>(result);
    }

    [Theory]
    [InlineData("Krystian", "Petek", "123321123", "krystianpetek2@gmail.com")]
    public async Task CustomersController_CreateCustomerAsync_Should_Return_Correct_StatusCode(string firstName, string lastName, string phoneNumber, string emailAddress)
    {
        // arrange
        _dispatcher.Setup(dispatcher => dispatcher.DispatchCommandAsync(It.IsAny<ICommand>(), It.IsAny<CancellationToken>()));

        // act
        CustomersController customersController = new CustomersController(_dispatcher.Object);
        ActionResult result = await customersController.CreateCustomerAsync(new CreateCustomerCommand
        {
            FirstName = firstName,
            LastName = lastName,
            PhoneNumber = phoneNumber,
            EmailAddress = emailAddress
        });

        // assert
        Assert.IsType<AcceptedResult>(result);
    }

    [Theory]
    [InlineData("Krystian", "Petek", "123321123", "krystianpetek2@gmail.com")]
    public async Task CustomersController_GetCustomerByUserIdAsync_Should_Return_Customer(string firstName, string lastName, string phoneNumber, string emailAddress)
    {
        // arrange
        Guid guid = Guid.NewGuid();
        _dispatcher.Setup(dispatcher => dispatcher.DispatchQueryAsync(It.IsAny<IQuery<GetCustomerByUserIdDto>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new GetCustomerByUserIdDto(guid, firstName, lastName, phoneNumber, emailAddress));

        // act
        CustomersController customersController = new CustomersController(_dispatcher.Object);
        ActionResult<GetCustomerByUserIdDto> result = await customersController.GetCustomerByUserIdAsync(guid);
        var resultValue = result.Result as OkObjectResult;

        // assert
        Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(guid, (resultValue?.Value as GetCustomerByUserIdDto)?.CustomerId);
        Assert.Equal(firstName, ((GetCustomerByUserIdDto)resultValue?.Value)?.FirstName);
        Assert.Equal(lastName, ((GetCustomerByUserIdDto)resultValue?.Value)?.LastName);
        Assert.Equal(phoneNumber, ((GetCustomerByUserIdDto)resultValue?.Value)?.PhoneNumber);
        Assert.Equal(emailAddress, ((GetCustomerByUserIdDto)resultValue?.Value)?.EmailAddress);
    }

    [Theory]
    [InlineData("Krystian", "Petek", "123321123", "krystianpetek2@gmail.com")]
    public async Task CustomersController_GetCustomerByIdAsync_Should_Return_WrongResult(string firstName, string lastName, string phoneNumber, string emailAddress)
    {
        // arrange
        Guid guid = Guid.NewGuid();
        _dispatcher.Setup(dispatcher => dispatcher.DispatchQueryAsync(It.IsAny<IQuery<GetCustomerByIdDto>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new GetCustomerByIdDto(guid, firstName, lastName, phoneNumber, emailAddress));

        // act
        CustomersController customersController = new CustomersController(_dispatcher.Object);
        ActionResult<GetCustomerByIdDto> result = await customersController.GetCustomerByIdAsync(Guid.NewGuid());
        var resultValue = result.Result as OkObjectResult;

        // assert
        Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(guid, (resultValue?.Value as GetCustomerByIdDto)?.CustomerId);
        Assert.Equal(firstName, ((GetCustomerByIdDto)resultValue?.Value)?.FirstName);
        Assert.Equal(lastName, ((GetCustomerByIdDto)resultValue?.Value)?.LastName);
        Assert.Equal(phoneNumber, ((GetCustomerByIdDto)resultValue?.Value)?.PhoneNumber);
        Assert.Equal(emailAddress, ((GetCustomerByIdDto)resultValue?.Value)?.EmailAddress);
    }
}
