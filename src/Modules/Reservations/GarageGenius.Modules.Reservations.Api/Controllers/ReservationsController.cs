using GarageGenius.Modules.Reservations.Application.Commands.AddReservation;
using GarageGenius.Shared.Abstractions.Dispatcher;
using GarageGenius.Shared.Infrastructure.SignalR.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace GarageGenius.Modules.Reservations.Api.Controllers;
public class ReservationsController : BaseController
{
    private readonly IDispatcher _dispatcher;
    private readonly IHubContext<NotificationsHub> _hubContextNotifications;

    public ReservationsController(IDispatcher dispatcher, IHubContext<NotificationsHub> hubContextNotifications)
    {
        _dispatcher = dispatcher;
        _hubContextNotifications = hubContextNotifications;
    }

    [HttpGet]
    public async Task<IActionResult> Get(string data)
    {
        await _hubContextNotifications.Clients.All.SendAsync("SendNotification", DateTime.Now, data);
        return Ok();
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddReservation(AddReservationCommand command)
    {
        await _dispatcher.DispatchCommandAsync(command);
        return Ok();
    }
}
