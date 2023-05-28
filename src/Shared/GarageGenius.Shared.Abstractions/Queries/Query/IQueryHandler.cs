namespace GarageGenius.Shared.Abstractions.Queries.Query;
public interface IQueryHandler<in TQuery, TQueryResult> where TQuery : IQuery<TQueryResult>
{
	Task<TQueryResult> HandleQueryAsync(TQuery query, CancellationToken cancellationToken = default);
}
