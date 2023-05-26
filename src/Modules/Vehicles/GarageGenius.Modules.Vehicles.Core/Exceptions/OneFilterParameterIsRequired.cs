using GarageGenius.Shared.Abstractions.Exceptions;

namespace GarageGenius.Modules.Vehicles.Core.Exceptions;
internal class OneFilterParameterIsRequired : GarageGeniusException
{
    public OneFilterParameterIsRequired() : base($"At least one filter parameter is required.")

    {
    }
}
