using GarageGenius.Shared.Abstractions.Exceptions;

namespace GarageGenius.Modules.Cars.Core.Exceptions;
internal sealed class InvalidProductionYearException : GarageGeniusException
{
    public InvalidProductionYearException(int? year) : base($"Invalid car production date year: {year}") { }
}