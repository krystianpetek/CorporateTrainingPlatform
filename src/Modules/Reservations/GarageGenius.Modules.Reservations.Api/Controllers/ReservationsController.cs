using GarageGenius.Modules.Reservations.Application.Commands.AddReservation;
using GarageGenius.Modules.Reservations.Application.Commands.UpdateReservation;
using GarageGenius.Modules.Reservations.Application.Queries.GetReservation;
using GarageGenius.Modules.Reservations.Application.Queries.GetReservationHistory;
using GarageGenius.Shared.Abstractions.Dispatcher;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GarageGenius.Modules.Reservations.Api.Controllers;
public class ReservationsController : BaseController
{
    private readonly IDispatcher _dispatcher;

    public ReservationsController(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpPut]
    [Authorize]
    [SwaggerOperation("Update reservation")]
    public async Task<ActionResult> UpdateReservation(UpdateReservationCommand command)
    {
        await _dispatcher.DispatchCommandAsync(command);
        return Ok();
    }

    [HttpPost]
    [Authorize]
    [SwaggerOperation("Add reservation")]
    public async Task<IActionResult> AddReservation(AddReservationCommand command)
    {
        await _dispatcher.DispatchCommandAsync(command);
        return Ok();
    }

    [HttpGet("{reservationId:guid}")]
    [Authorize]
    [SwaggerOperation("Get reservation")]
    public async Task<ActionResult> GetReservationAsync(Guid reservationId)
    {
        GetReservationQuery query = new GetReservationQuery(reservationId);
        GetReservationQueryDto? getReservationQueryDto = await _dispatcher.DispatchQueryAsync(query);
        return Ok(getReservationQueryDto);
    }

    [HttpGet("{reservationId:guid}/history")]
    [Authorize]
    [SwaggerOperation("Get reservation history")]
    public async Task<ActionResult> GetReservationHistoryAsync(Guid reservationId)
    {
		GetReservationHistoryQuery query = new GetReservationHistoryQuery(reservationId);
		IReadOnlyList<GetReservationHistoryQueryDto> getReservationHistoryQueryDto = await _dispatcher.DispatchQueryAsync(query);
		return Ok(getReservationHistoryQueryDto);
    }
}
