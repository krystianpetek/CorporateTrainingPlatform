using GarageGenius.Shared.Abstractions.Exceptions;

namespace GarageGenius.Modules.Cars.Core.Exceptions;
internal sealed class InvalidManufacturerException : GarageGeniusException
{
    public InvalidManufacturerException(string manufacturer) : base($"Invalid manufacturer name: {manufacturer}") { }
}