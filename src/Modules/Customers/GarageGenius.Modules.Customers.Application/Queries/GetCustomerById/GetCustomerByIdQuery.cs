using GarageGenius.Shared.Abstractions.Queries.Query;

namespace GarageGenius.Modules.Customers.Application.Queries.GetCustomerById;
internal record GetCustomerByIdQuery : IQuery<GetCustomerByIdDto>
{
	public Guid Id { get; init; }

	public GetCustomerByIdQuery(Guid id)
	{
		Id = id;
	}
}
