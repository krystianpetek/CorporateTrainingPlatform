using GarageGenius.Shared.Abstractions.Queries.PagedQuery;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GarageGenius.Modules.Reservations.Application.Queries.GetCustomerReservations;
public sealed record class GetCustomerReservationsQuery : IPagedQuery<GetCustomerReservationsQueryDto>
{
	[Required]
	public Guid CustomerId { get; set; }
	
	[DefaultValue(false)]
    public bool OnlyPending { get; init; }

    [DefaultValue(1)]
	public int PageNumber { get; init; }

	[DefaultValue(10)]
	public int PageSize { get; init; }

	public GetCustomerReservationsQuery() { }

	public GetCustomerReservationsQuery(Guid customerId, int pageNumber, int pageSize)
	{
		CustomerId = customerId;
		PageNumber = pageNumber;
		PageSize = pageSize;
	}
}
