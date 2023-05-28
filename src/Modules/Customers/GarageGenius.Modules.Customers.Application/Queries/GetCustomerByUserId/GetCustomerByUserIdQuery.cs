using GarageGenius.Shared.Abstractions.Queries.Query;

namespace GarageGenius.Modules.Customers.Application.Queries.GetCustomerByUserId;
internal record GetCustomerByUserIdQuery : IQuery<GetCustomerByUserIdDto>
{
	public Guid UserId { get; init; }

	public GetCustomerByUserIdQuery(Guid userId)
	{
		UserId = userId;
	}
}
