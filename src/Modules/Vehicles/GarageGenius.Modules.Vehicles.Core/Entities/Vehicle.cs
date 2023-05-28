using GarageGenius.Modules.Vehicles.Core.Types;
using GarageGenius.Modules.Vehicles.Core.ValueObjects;
using GarageGenius.Shared.Abstractions.Common;

namespace GarageGenius.Modules.Vehicles.Core.Entities;
internal sealed class Vehicle : AuditableEntity
{
	internal VehicleId VehicleId { get; private set; }
	public CustomerId CustomerId { get; private set; }
	public Manufacturer Manufacturer { get; private set; }
	public Model Model { get; private set; }
	public LicensePlate LicensePlate { get; private set; }
	public Vin? Vin { get; private set; }
	public Year? Year { get; private set; }

	private Vehicle() { }

	public Vehicle(CustomerId customerId, Manufacturer manufacturer, Model model, LicensePlate licensePlate, Vin? vin, Year? year)
	{
		VehicleId = new VehicleId(Guid.NewGuid());
		CustomerId = customerId;
		Manufacturer = manufacturer;
		Model = model;
		LicensePlate = licensePlate;
		Vin = vin;
		Year = year;
	}

	internal void UpdateVehicle(Vin? vin, Year? year)
	{
		Year = year;
		Vin = vin;
	}

	internal void ChangeLicensePlate(LicensePlate licensePlate)
	{
		LicensePlate = licensePlate;
	}

	internal void ChangeOwner(CustomerId customerId)
	{
		CustomerId = customerId;
	}
}
