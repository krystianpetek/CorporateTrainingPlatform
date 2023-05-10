using Microsoft.AspNetCore.Mvc;

namespace GarageGenius.Modules.Cars.Api.Controllers;

[ApiController]
[Route($"{CarsModule.BasePath}/[controller]")]
public abstract class BaseController : ControllerBase { }