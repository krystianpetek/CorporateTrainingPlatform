using GarageGenius.Shared.Abstractions.Dispatcher;

namespace GarageGenius.Modules.Cars.Api.Controllers;
public class CarsController : BaseController
{
    private readonly IDispatcher _dispatcher;

    public CarsController(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }
}
