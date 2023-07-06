using Xunit;
using GarageGenius.Modules.Customers.Core.Entities;
using GarageGenius.Modules.Customers.Core.Types;
using GarageGenius.Modules.Customers.Core.ValueObjects;

namespace GarageGenius.Modules.Customers.UnitTests;

public class CustomerTests
{
	[Fact]
	public async Task Create_Customer_()
	{
		// Arrange
		var id = Guid.NewGuid();
		var customerId = new CustomerId(id);
		var userId = new UserId(id);
		var emailAddress = new EmailAddress("krystianpetek2@gmail.com");
		var customer = new Customer(customerId, userId, emailAddress);
		
		Assert.Equal(customerId, customer.CustomerId);
		Assert.Equal(userId, customer.UserId);
		Assert.Equal(emailAddress, customer.EmailAddress);
	}
}