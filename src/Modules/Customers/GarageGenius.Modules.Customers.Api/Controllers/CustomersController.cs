using GarageGenius.Shared.Abstractions.Dispatcher;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GarageGenius.Modules.Customers.Api.Controllers;
public class CustomersController : BaseController
{
    private readonly IDispatcher _dispatcher;

    public CustomersController(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }
}
