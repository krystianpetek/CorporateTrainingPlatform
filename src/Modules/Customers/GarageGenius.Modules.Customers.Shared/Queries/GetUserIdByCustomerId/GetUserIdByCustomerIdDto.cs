namespace GarageGenius.Modules.Customers.Shared.Queries.GetUserIdByCustomerId;
public record GetUserIdByCustomerIdDto
{
	public Guid? UserId { get; init; }

	public GetUserIdByCustomerIdDto(Guid? userId)
	{
		UserId = userId;
	}
}