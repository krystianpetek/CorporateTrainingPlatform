using GarageGenius.Modules.Cars.Core.Types;
using GarageGenius.Modules.Cars.Core.ValueObjects;
using GarageGenius.Shared.Abstractions.Common;

namespace GarageGenius.Modules.Cars.Core.Entities;
internal sealed class Car : AuditableEntity
{
    public CarId Id { get; private set; }
    public CustomerId CustomerId { get; private set; }
    public Manufacturer Manufacturer { get; private set; }
    public Model Model { get; private set; }
    public Year? Year { get; private set; }
    public Vin? Vin { get; private set; }
    public LicensePlate? LicensePlate { get; private set; }

    private Car() { }

    public Car(CustomerId customerId, Manufacturer manufacturer, Model model)
    {
        Id = new CarId(Guid.NewGuid());
        CustomerId = customerId;
        Manufacturer = manufacturer;
        Model = model;
    }

    public Car(CustomerId customerId, Manufacturer manufacturer, Model model, Year? year, Vin? vin, LicensePlate? licensePlate)
    {
        Id = new CarId(Guid.NewGuid());
        CustomerId = customerId;
        Manufacturer = manufacturer;
        Model = model;
        Year = year;
        Vin = vin;
        LicensePlate = licensePlate;
    }
}
