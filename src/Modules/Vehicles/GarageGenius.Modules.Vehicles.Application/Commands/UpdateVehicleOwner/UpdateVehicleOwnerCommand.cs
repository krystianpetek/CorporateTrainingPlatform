using GarageGenius.Shared.Abstractions.Commands;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GarageGenius.Modules.Vehicles.Application.Commands.UpdateVehicleOwner;
public record class UpdateVehicleOwnerCommand : ICommand
{
    [JsonIgnore]
    public Guid VehicleId { get; set; }
    
    [Required]
    public Guid CustomerId { get; init; }
    
    public UpdateVehicleOwnerCommand(Guid VehicleId, Guid CustomerId)
    {
        this.VehicleId = VehicleId;
        this.CustomerId = CustomerId;
    }

    public UpdateVehicleOwnerCommand()
    {
    }
}
