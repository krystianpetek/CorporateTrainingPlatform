using GarageGenius.Modules.Cars.Core.Entities;

namespace GarageGenius.Modules.Cars.Application.Dto;
public record GetCarDto
{
    public GetCarDto(Guid carId, string manufacturer, string model, int? year, string licensePlate)
    {
        Id = carId;
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


public static class UserExtensions
{
    internal static GetCarDto AsGetUserDto(this Car car)
    {
        return new GetCarDto(car.Id, car.Manufacturer, car.Model, car.Year, car.LicensePlate);
    }
}