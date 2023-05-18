namespace GarageGenius.Modules.Vehicles.Application.Dto;
public record GetVehicleDto
{
    public GetVehicleDto(Guid vehicleId, string manufacturer, string model, int? year, string licensePlate)
    {
        Id = vehicleId;
        Manufacturer = manufacturer;
        Model = model;
        Year = year;
        LicensePlate = licensePlate;
    }
    public Guid Id { get; init; }
    public string Manufacturer { get; init; }
    public string Model { get; init; }
    public int? Year { get; init; }
    public string LicensePlate { get; init; }
}