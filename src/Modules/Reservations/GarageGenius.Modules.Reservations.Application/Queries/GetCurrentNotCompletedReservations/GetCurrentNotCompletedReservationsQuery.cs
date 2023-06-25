using GarageGenius.Shared.Abstractions.Queries.PagedQuery;
using System.ComponentModel;

namespace GarageGenius.Modules.Reservations.Application.Queries.GetCurrentNotCompletedReservations;
public sealed record class GetCurrentNotCompletedReservationsQuery : IPagedQuery<GetCurrentNotCompletedReservationsQueryDto>
{
	[DefaultValue(1)]
	public int PageNumber { get; init; }

	[DefaultValue(10)]
	public int PageSize { get; init; }

	public GetCurrentNotCompletedReservationsQuery() { }

	public GetCurrentNotCompletedReservationsQuery(int PageNumber, int PageSize)
	{
		this.PageNumber = PageNumber;
		this.PageSize = PageSize;
	}
}
