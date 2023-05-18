using GarageGenius.Shared.Abstractions.Commands;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GarageGenius.Modules.Cars.Application.Commands.AddCar;
public record AddCarCommand : ICommand
{
    [JsonIgnore]
    public Guid CustomerId { get; set; }

    [Required] // TODO validation for command and queries on controllers ?
    public string Manufacturer { get; init; }

    [Required]
    public string Model { get; init; }

    [Required]
    public string LicensePlate { get; init; }

    public int? Year { get; init; }

    public string? Vin { get; init; }
}

