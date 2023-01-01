using Microsoft.AspNetCore.Mvc;

namespace CorporateTrainingPlatform.Controllers;

[ApiController]
[Route("api/weather")]
public class WeatherController
{
    private static readonly string[] Weathers = new string[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };

    private readonly ILogger<WeatherController> _logger;

    public WeatherController(ILogger<WeatherController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<WeatherForecast> GetWeathers()
    {
        var forecast = Enumerable.Range(1, 5).Select((int index) =>
        new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Weathers[Random.Shared.Next(Weathers.Length)]
        }).ToArray();
        return forecast;
    }

}
