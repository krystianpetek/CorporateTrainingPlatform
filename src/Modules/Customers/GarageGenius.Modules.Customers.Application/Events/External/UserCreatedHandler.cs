using GarageGenius.Modules.Customers.Core.Entities;
using GarageGenius.Modules.Customers.Core.Repositories;
using GarageGenius.Modules.Users.Shared.Events;
using GarageGenius.Shared.Abstractions.Events;

namespace GarageGenius.Modules.Customers.Application.Events.External;
internal sealed class UserCreatedHandler : IEventHandler<UserCreated>
{
    private readonly ICustomerRepository _customerRepository;

    public UserCreatedHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task HandleAsync(UserCreated @event, CancellationToken cancellationToken = default)
    {
        await _customerRepository.AddAsync(new Customer(@event.UserId, @event.Email));
    }
}