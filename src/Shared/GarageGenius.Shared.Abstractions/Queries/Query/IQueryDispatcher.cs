namespace GarageGenius.Shared.Abstractions.Queries.Query;
public interface IQueryDispatcher
{
	Task<TQueryResult> DispatchQueryAsync<TQueryResult>(IQuery<TQueryResult> query, CancellationToken cancellationToken = default);
}
