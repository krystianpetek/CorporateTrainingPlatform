using GarageGenius.Modules.Customers.Core.Entities;
using GarageGenius.Modules.Customers.Core.Exceptions;
using GarageGenius.Modules.Customers.Core.Repositories;
using GarageGenius.Modules.Customers.Infrastructure.Persistance.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace GarageGenius.Modules.Customers.Infrastructure.Persistance.Repositories;
internal class CustomerRepository : ICustomerRepository
{
    private readonly CustomersDbContext _customersDbContext;

    public CustomerRepository(CustomersDbContext customersDbContext)
    {
        _customersDbContext = customersDbContext;
    }

    public async Task AddCustomerAsync(Customer customer, CancellationToken cancellationToken = default)
    {
        await _customersDbContext.Customers.AddAsync(customer, cancellationToken);
        await _customersDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<Customer?> GetCustomerByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Customer? customer = await _customersDbContext.Customers
            .AsQueryable()
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken) ?? throw new CustomerNotFoundException(id);

        return customer;
    }

    public async Task<Customer?> GetCustomerByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        Customer? customer = await _customersDbContext.Customers
            .AsQueryable()
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.UserId == userId, cancellationToken) ?? throw new CustomerNotFoundException(userId);

        return customer;
    }

    public async Task UpdateCustomerAsync(Customer customer, CancellationToken cancellationToken = default)
    {
        _customersDbContext.Customers.Update(customer);
        await _customersDbContext.SaveChangesAsync(cancellationToken);
    }
}
