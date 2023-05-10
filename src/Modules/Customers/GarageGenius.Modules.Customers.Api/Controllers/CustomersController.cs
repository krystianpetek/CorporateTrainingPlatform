using GarageGenius.Shared.Abstractions.Dispatcher;

namespace GarageGenius.Modules.Customers.Api.Controllers;
public class CustomersController : BaseController
{
    private readonly IDispatcher _dispatcher;

    public CustomersController(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }
}
