using GarageGenius.Shared.Abstractions.Queries.PagedQuery;
using System.ComponentModel;
using GarageGenius.Modules.Reservations.Core.Reservations.ValueObjects;

namespace GarageGenius.Modules.Reservations.Application.Queries.GetCurrentNotCompletedReservations;
public sealed record class GetCurrentNotCompletedReservationsQuery : IPagedQuery<GetCurrentNotCompletedReservationsQueryDto>
{
	[DefaultValue(1)]
	public int PageNumber { get; init; }

	[DefaultValue(10)]
	public int PageSize { get; init; }

	[DefaultValue(false)]
	public bool ToDecision { get; init; }

	public GetCurrentNotCompletedReservationsQuery()
	{ }

	public GetCurrentNotCompletedReservationsQuery(int PageNumber, int PageSize, bool ToDecision)
	{
		this.PageNumber = PageNumber;
		this.PageSize = PageSize;
		this.ToDecision = ToDecision;
	}
}
