using Microsoft.AspNetCore.Mvc;

namespace GarageGenius.Modules.Reservations.Api.Controllers;

[ApiController]
[Route($"{ReservationsModule.BasePath}/[controller]")]
public abstract class BaseController : ControllerBase { }