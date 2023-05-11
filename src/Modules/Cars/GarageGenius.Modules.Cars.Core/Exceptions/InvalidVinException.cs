using GarageGenius.Shared.Abstractions.Exceptions;

namespace GarageGenius.Modules.Cars.Core.Exceptions;
internal sealed class InvalidVinException : GarageGeniusException
{
    public InvalidVinException(string vin) : base($"Invalid car vin number: {vin}") { }
}