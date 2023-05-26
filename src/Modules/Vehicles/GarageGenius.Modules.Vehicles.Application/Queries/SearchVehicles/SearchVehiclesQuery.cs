using GarageGenius.Modules.Vehicles.Core.Models;
using GarageGenius.Shared.Abstractions.Queries;

namespace GarageGenius.Modules.Vehicles.Application.Queries.SearchVehiclesQuery;
public record SearchVehiclesQuery : IQuery<IReadOnlyList<SearchVehiclesQueryDto>>
{
    public SearchVehiclesParameters SearchVehiclesParameters { get; init; }

    public SearchVehiclesQuery(SearchVehiclesParameters SearchVehiclesParameters)
    {
        this.SearchVehiclesParameters = SearchVehiclesParameters;
    }
}