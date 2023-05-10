using Microsoft.AspNetCore.Mvc;

namespace GarageGenius.Modules.Customers.Api.Controllers;

[ApiController]
[Route($"{CustomersModule.BasePath}/[controller]")]
public abstract class BaseController : ControllerBase { }