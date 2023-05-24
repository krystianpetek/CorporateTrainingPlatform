using GarageGenius.Modules.Vehicles.Core.ValueObjects;

namespace GarageGenius.Modules.Vehicles.Core.Models;

public class GetVehicleFilterParameters
{
    public GetVehicleFilterParameters(string? vin, string? licensePlate)
    {
        LicensePlate = licensePlate;
        Vin = vin;
    }

    public GetVehicleFilterParameters() { }

    public string? Vin { get => _vin; set => _vin = value; }
    public string? LicensePlate { get => _licensePlate; set => _licensePlate = value; }

    private Vin? _vin { get; set; }
    private LicensePlate? _licensePlate { get; set; }
}
