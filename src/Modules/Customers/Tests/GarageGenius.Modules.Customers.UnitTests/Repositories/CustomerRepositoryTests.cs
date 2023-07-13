using GarageGenius.Modules.Customers.Core.Entities;
using GarageGenius.Modules.Customers.Core.Repositories;
using GarageGenius.Modules.Customers.UnitTests.Helpers;
using Xunit;

namespace GarageGenius.Modules.Customers.UnitTests.Repository;
public class CustomerRepositoryTests
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerRepositoryTests()
    {
        _customerRepository = new TestCustomerRepository();
    }

    [Fact]
    public async Task CreateCustomer_Should_AddCustomerToDatabaseUsingRepository()
    {
        // arrange
        Customer customer = new Customer("Krystian", "Petek", "123123123", "krystianpetek2@gmail.com");

        // act
        await _customerRepository.AddCustomerAsync(customer);
        Customer? customerResult = await _customerRepository.GetCustomerByIdAsync(customer.CustomerId);

        // assert
        Assert.Equal(customer, customerResult);
    }

    [Fact]
    public async Task CreateCustomerWithIdentity_Should_AddCustomerToDatabaseUsingRepository()
    {
        // arrange
        Customer customer = CreateCustomer();

        // act
        await _customerRepository.AddCustomerAsync(customer);
        Customer? customerResult = await _customerRepository.GetCustomerByUserIdAsync(customer.UserId.Value);

        // assert
        Assert.Equal(customer, customerResult);
    }

    [Fact]
    public async Task UpdateCustomer_Should_UpdateCustomerInDatabaseUsingRepository()
    {
        // arrange
        Customer customer = CreateCustomer();
        await _customerRepository.AddCustomerAsync(customer);

        // act
        customer.UpdateCustomer("Krystian", "Petek", "123123123");
        await _customerRepository.UpdateCustomerAsync(customer);
        Customer? customerResult = await _customerRepository.GetCustomerByIdAsync(customer.CustomerId);

        // assert
        Assert.Equal(customer, customerResult);
    }

    private Customer CreateCustomer()
    {
        Guid guid = Guid.NewGuid();
        return new Customer(guid, guid, "krystianpetek2@gmail.com");
    }
}
