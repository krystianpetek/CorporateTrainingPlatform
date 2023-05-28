using GarageGenius.Shared.Abstractions.Queries.Query;

namespace GarageGenius.Modules.Reservations.Application.Queries.GetReservationHistory;
public sealed record class GetReservationHistoryQuery : IQuery<GetReservationHistoryQueryDtos>
{
	public Guid ReservationId { get; init; }
	public GetReservationHistoryQuery(Guid ReservationId)
	{
		this.ReservationId = ReservationId;
	}
}
