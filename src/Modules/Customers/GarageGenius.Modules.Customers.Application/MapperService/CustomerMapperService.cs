using GarageGenius.Modules.Customers.Application.Commands.CreateCustomer;
using GarageGenius.Modules.Customers.Application.Queries.GetCustomerById;
using GarageGenius.Modules.Customers.Application.Queries.GetCustomerByUserId;
using GarageGenius.Modules.Customers.Core.Entities;
using GarageGenius.Modules.Customers.Shared.Queries.GetUserIdByCustomerId;
using GarageGenius.Modules.Users.Shared.Events;

namespace GarageGenius.Modules.Customers.Application.MapperService;
internal interface ICustomerMapperService
{
	public Customer MapToCustomer(CreateCustomerCommand createCustomerCommand);
	public Customer MapToCustomer(UserCreatedEvent userCreated);
	public GetCustomerByIdDto MapToGetCustomerByIdDto(Customer? entity);
	public GetCustomerByUserIdDto MapToGetCustomerByUserIdDto(Customer? entity);
	public GetUserIdByCustomerIdDto MapToGetUserIdByCustomerIdDto(Customer? entity);
	//public Customer MapToCustomer(GetCustomerByIdDto dto);
	//public Customer MapToCustomer(GetCustomerByUserIdDto dto);
	//public Customer MapToCustomer(GetUserIdByCustomerIdDto dto);
}

internal class CustomerMapperService : ICustomerMapperService
{
	public Customer MapToCustomer(CreateCustomerCommand createCustomerCommand)
	{
		return new Customer(createCustomerCommand?.FirstName, createCustomerCommand?.LastName, createCustomerCommand?.PhoneNumber, createCustomerCommand?.EmailAddress);
	}

	public Customer MapToCustomer(UserCreatedEvent userCreated)
	{
		return new Customer(userCreated?.CustomerId, userCreated?.UserId, userCreated.EmailAddress);
	}

	public GetCustomerByIdDto MapToGetCustomerByIdDto(Customer? entity)
	{
		return new GetCustomerByIdDto(entity?.CustomerId, entity?.FirstName, entity?.LastName, entity?.PhoneNumber, entity?.EmailAddress);
	}

	public GetCustomerByUserIdDto MapToGetCustomerByUserIdDto(Customer? entity)
	{
		return new GetCustomerByUserIdDto(entity?.CustomerId, entity?.FirstName, entity?.LastName, entity?.PhoneNumber, entity?.EmailAddress);
	}

	public GetUserIdByCustomerIdDto MapToGetUserIdByCustomerIdDto(Customer? entity)
	{
		return new GetUserIdByCustomerIdDto(entity?.UserId);
	}
}
