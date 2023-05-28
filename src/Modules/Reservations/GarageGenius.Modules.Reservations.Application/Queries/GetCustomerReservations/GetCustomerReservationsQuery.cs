using GarageGenius.Shared.Abstractions.Queries;

namespace GarageGenius.Modules.Reservations.Application.Queries.GetCustomerReservations;
public sealed record class GetCustomerReservationsQuery : IQuery<GetCustomerReservationsQueryDto>
{
	public Guid CustomerId { get; init; }
	public GetCustomerReservationsQuery(Guid CustomerId)
	{
		this.CustomerId = CustomerId;
	}
}
