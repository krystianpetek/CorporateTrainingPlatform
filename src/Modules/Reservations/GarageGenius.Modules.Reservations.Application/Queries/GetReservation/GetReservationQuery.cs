using GarageGenius.Shared.Abstractions.Queries.Query;

namespace GarageGenius.Modules.Reservations.Application.Queries.GetReservation;
public sealed record class GetReservationQuery : IQuery<GetReservationQueryDto>
{
	public Guid ReservationId { get; init; }
	public GetReservationQuery(Guid ReservationId)
	{
		this.ReservationId = ReservationId;
	}
}
