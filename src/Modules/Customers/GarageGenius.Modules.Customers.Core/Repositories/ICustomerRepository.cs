using GarageGenius.Modules.Customers.Core.Entities;

namespace GarageGenius.Modules.Customers.Core.Repositories;
internal interface ICustomerRepository
{
    Task<Customer?> GetCustomerByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Customer?> GetCustomerByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task AddCustomerAsync(Customer customer, CancellationToken cancellationToken = default);
    Task UpdateCustomerAsync(Customer customer, CancellationToken cancellationToken = default);

}
