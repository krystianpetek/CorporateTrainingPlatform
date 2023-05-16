using GarageGenius.Modules.Customers.Core.Entities;

namespace GarageGenius.Modules.Customers.Core.Repositories;
internal interface ICustomerRepository
{
    Task<Customer?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(Customer customer, CancellationToken cancellationToken = default);
    Task UpdateAsync(Customer customer, CancellationToken cancellationToken = default);

}
