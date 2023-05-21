using GarageGenius.Shared.Abstractions.Dispatcher;
using GarageGenius.Shared.Infrastructure.SignalR.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace GarageGenius.Modules.Booking.Api.Controllers;
public class BookingController : BaseController
{
    private readonly IDispatcher _dispatcher;
    private readonly IHubContext<NotificationsHub> _hubContextNotifications;

    public BookingController(IDispatcher dispatcher, IHubContext<NotificationsHub> hubContextNotifications)
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

}
