using GarageGenius.Shared.Abstractions.Exceptions;

namespace GarageGenius.Modules.Vehicles.Core.Exceptions;
internal sealed class VehicleNotFoundException : GarageGeniusException
{
    public VehicleNotFoundException(Guid id) : base($"Vehicle with ID: '{id}' was not found.") { }
    public VehicleNotFoundException(string vin) : base($"Vehicle with VIN number: '{vin}' was not found.") { }
}
