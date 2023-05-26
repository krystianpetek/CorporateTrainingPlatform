using GarageGenius.Shared.Abstractions.Commands;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GarageGenius.Modules.Vehicles.Application.Commands.AddVehicle;
public record AddVehicleCommand : ICommand
{
    [JsonIgnore]
    public Guid CustomerId { get; set; }

    [Required] // TODO validation for command and queries on controllers ?
    [DefaultValue("Renault")]
    public string Manufacturer { get; init; }

    [Required]
    [DefaultValue("Clio")]
    public string Model { get; init; }

    [Required]
    [DefaultValue("KWA00000")]
    public string LicensePlate { get; init; }

    [DefaultValue(2002)]
    public int? Year { get; init; }

    [DefaultValue("VF1BB0A0523456789")]
    public string? Vin { get; init; }
}

