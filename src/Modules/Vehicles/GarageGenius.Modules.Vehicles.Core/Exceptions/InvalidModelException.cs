using GarageGenius.Shared.Abstractions.Exceptions;

namespace GarageGenius.Modules.Vehicles.Core.Exceptions;
internal sealed class InvalidModelException : GarageGeniusException
{
    public InvalidModelException(string model) : base($"Invalid model name: {model}") { }
}