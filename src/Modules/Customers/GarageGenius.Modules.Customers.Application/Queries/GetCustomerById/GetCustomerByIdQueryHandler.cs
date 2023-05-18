using GarageGenius.Modules.Customers.Application.Queries.GetCustomerByUserId;
using GarageGenius.Modules.Customers.Core.Entities;
using GarageGenius.Modules.Customers.Core.Repositories;
using GarageGenius.Shared.Abstractions.Queries;

namespace GarageGenius.Modules.Customers.Application.Queries.GetCustomerById;
internal class GetCustomerByIdQueryHandler : IQueryHandler<GetCustomerByIdQuery, GetCustomerByIdDto>
{
    private readonly ICustomerRepository _customerRepository;

    public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<GetCustomerByIdDto> HandleAsync(GetCustomerByIdQuery query, CancellationToken cancellationToken = default)
    {
        Customer? customer = await _customerRepository.GetCustomerByIdAsync(query.Id);
        return new GetCustomerByIdDto(customer.Id, customer.FirstName, customer.LastName, customer.PhoneNumber, customer.EmailAddress);
    }
}
