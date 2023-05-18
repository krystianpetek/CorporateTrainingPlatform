using GarageGenius.Modules.Vehicles.Core.Types;
using GarageGenius.Modules.Vehicles.Core.ValueObjects;
using GarageGenius.Shared.Abstractions.Common;

namespace GarageGenius.Modules.Vehicles.Core.Entities;
internal sealed class Vehicle : AuditableEntity
{
    public VehicleId Id { get; private set; }
    public CustomerId CustomerId { get; private set; }
    public Manufacturer Manufacturer { get; private set; }
    public Model Model { get; private set; }
    public LicensePlate LicensePlate { get; private set; }
    public Year? Year { get; private set; }
    public Vin? Vin { get; private set; }

    private Vehicle() { }

    public Vehicle(CustomerId customerId, Manufacturer manufacturer, Model model, LicensePlate licensePlate)
    {
        Id = new VehicleId(Guid.NewGuid());
        CustomerId = customerId;
        Manufacturer = manufacturer;
        Model = model;
        LicensePlate = licensePlate;
    }

    public Vehicle(CustomerId customerId, Manufacturer manufacturer, Model model, LicensePlate licensePlate, Year? year, Vin? vin) : this(customerId, manufacturer, model, licensePlate)
    {
        Year = year;
        Vin = vin;
    }

    public void UpdateVehicle(Year? year, Vin? vin)
    {
        Year = year;
        Vin = vin;
    }
}
