using GarageGenius.Modules.Reservations.Application.Commands.AddReservation;
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
}
