using GarageGenius.Modules.Cars.Application.Dto;
using GarageGenius.Shared.Abstractions.Queries;

namespace GarageGenius.Modules.Cars.Application.Queries.GetCarQuery;
public record GetCarQuery : IQuery<GetCarDto>
{
    public Guid CarId { get; init; }

    public GetCarQuery(Guid CarId)
    {
        this.CarId = CarId;
    }
}
