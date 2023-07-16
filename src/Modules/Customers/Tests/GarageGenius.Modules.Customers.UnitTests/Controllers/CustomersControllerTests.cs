using GarageGenius.Modules.Customers.Api.Controllers;
using GarageGenius.Modules.Customers.Application.Queries.GetCustomerById;
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
    public async Task CustomersController_GetCustomerByIdAsync_Should_Return_Customer(string firstName, string lastName, string phoneNumber, string emailAddress)
    {
        // arrange
        Guid guid = Guid.NewGuid();
        _dispatcher.Setup(dispatcher => dispatcher.DispatchQueryAsync(It.IsAny<IQuery<GetCustomerByIdDto>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new GetCustomerByIdDto(guid, firstName, lastName, phoneNumber, emailAddress));

        // act
        CustomersController customersController = new CustomersController(_dispatcher.Object);
        ActionResult<GetCustomerByIdDto> result = await customersController.GetCustomerByIdAsync(guid);
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
