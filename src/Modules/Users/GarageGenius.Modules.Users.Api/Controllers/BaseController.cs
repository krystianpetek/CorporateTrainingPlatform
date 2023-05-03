using Microsoft.AspNetCore.Mvc;

namespace GarageGenius.Modules.Users.Api.Controllers;

[ApiController]
[Route($"{UsersModule.BasePath}/[controller]")]
public abstract class BaseController : ControllerBase { }