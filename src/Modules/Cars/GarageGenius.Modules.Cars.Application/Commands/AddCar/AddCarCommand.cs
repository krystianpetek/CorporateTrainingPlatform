using GarageGenius.Shared.Abstractions.Commands;
using System.Text.Json.Serialization;

namespace GarageGenius.Modules.Cars.Application.Commands.AddCar;
public record AddCarCommand : ICommand
{
    [JsonIgnore]
    public Guid CustomerId { get; set; }
    public string Manufacturer { get; init; } = string.Empty;
    public string Model { get; init; } = string.Empty;
    public string LicensePlate { get; init; } = string.Empty;
    public int? Year { get; init; }
    public string? Vin { get; init; }
}

