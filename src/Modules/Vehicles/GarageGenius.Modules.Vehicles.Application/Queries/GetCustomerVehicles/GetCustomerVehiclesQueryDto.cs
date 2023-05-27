namespace GarageGenius.Modules.Vehicles.Application.Queries.GetCustomerVehicles;
public record GetCustomerVehiclesQueryDto
{
    public GetCustomerVehiclesQueryDto(Guid vehicleId, string manufacturer, string model, int? year, string licensePlate, string vin)
    {
        Id = vehicleId;
        Manufacturer = manufacturer;
        Model = model;
        Year = year;
        LicensePlate = licensePlate;
        Vin = vin;
    }
    public Guid Id { get; init; }
    public string Manufacturer { get; init; }
    public string Model { get; init; }
    public int? Year { get; init; }
    public string LicensePlate { get; init; }
    public string Vin { get; init; }
}