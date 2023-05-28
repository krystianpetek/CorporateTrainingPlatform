using GarageGenius.Shared.Abstractions.Queries;
using System.Text.Json.Serialization;

namespace GarageGenius.Modules.Reservations.Application.Queries.GetReservation;
public sealed record class GetReservationQuery : IQuery<GetReservationQueryDto>
{
	public Guid ReservationId { get; init; }
	public GetReservationQuery(Guid CustomerId)
	{
		this.ReservationId = CustomerId;
	}
}
