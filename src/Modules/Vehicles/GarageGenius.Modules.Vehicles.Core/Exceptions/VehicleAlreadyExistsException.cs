using GarageGenius.Shared.Abstractions.Exceptions;

namespace GarageGenius.Modules.Vehicles.Core.Exceptions;
internal class VehicleAlreadyExistsException : GarageGeniusException
{
	public VehicleAlreadyExistsException(string vin) : base($"Vehicle with VIN number: {vin} already exists.", System.Net.HttpStatusCode.Conflict)
	{
	}
}
