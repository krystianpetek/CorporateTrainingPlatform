using GarageGenius.Modules.Vehicles.Core.ValueObjects;
using System.ComponentModel;

namespace GarageGenius.Modules.Vehicles.Core.Models;

public class SearchVehiclesParameters
{
	public SearchVehiclesParameters(string? vin, string? licensePlate)
	{
		LicensePlate = licensePlate;
		Vin = vin;
	}

	public SearchVehiclesParameters() { }

	[DefaultValue("VF1BB0A0523456789")]
	public string? Vin { get => _vin; set => _vin = value; }
	[DefaultValue("KWA00000")]
	public string? LicensePlate { get => _licensePlate; set => _licensePlate = value; }

	private Vin? _vin { get; set; }
	private LicensePlate? _licensePlate { get; set; }
}
