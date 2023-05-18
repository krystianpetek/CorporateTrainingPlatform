using GarageGenius.Shared.Abstractions.Dispatcher;

namespace GarageGenius.Modules.Booking.Api.Controllers;
public class BookingController : BaseController
{
    private readonly IDispatcher _dispatcher;

    public BookingController(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }
}
