using GarageGenius.Shared.Abstractions.Common;

namespace GarageGenius.Modules.Cars.Core.Entities;
internal sealed class Car : AuditableEntity
{
    public Guid Id { get; private set; }
    public Guid CustomerId { get; private set; }
    public string Manufacturer { get; private set; }
    public string Model { get; private set; }
    public string? Year { get; private set; }
    public string? Vin { get; private set; }
    public string? LicensePlate { get; private set; }

    private Car() { }

    public Car(Guid customerId, string manufacturer, string model)
    {
        Id = Guid.NewGuid();
        CustomerId = customerId;
        Manufacturer = manufacturer;
        Model = model;
    }

    public Car(Guid customerId, string manufacturer, string model, string? year, string? vin, string? licensePlate)
    {
        Id = Guid.NewGuid();
        CustomerId = customerId;
        Manufacturer = manufacturer;
        Model = model;
        Year = year;
        Vin = vin;
        LicensePlate = licensePlate;
    }
}
