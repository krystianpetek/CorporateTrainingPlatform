using GarageGenius.Shared.Abstractions.Exceptions;

namespace GarageGenius.Modules.Vehicles.Core.Exceptions;
internal sealed class InvalidProductionYearException : GarageGeniusException
{
	public InvalidProductionYearException(int? year) : base($"Invalid vehicle production date year: {year}") { }
}