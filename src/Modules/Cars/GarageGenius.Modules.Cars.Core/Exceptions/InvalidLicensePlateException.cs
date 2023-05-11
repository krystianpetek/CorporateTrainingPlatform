using GarageGenius.Shared.Abstractions.Exceptions;

namespace GarageGenius.Modules.Cars.Core.Exceptions;
internal sealed class InvalidLicensePlateException : GarageGeniusException
{
    public InvalidLicensePlateException(string licensePlate) : base($"License plate: {licensePlate} is invalid.") { }
}