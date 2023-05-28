using GarageGenius.Shared.Abstractions.Exceptions;

namespace GarageGenius.Modules.Vehicles.Core.Exceptions;
internal sealed class InvalidVinException : GarageGeniusException
{
	public InvalidVinException(string vin) : base($"Invalid vehicle vin number: {vin}") { }
}