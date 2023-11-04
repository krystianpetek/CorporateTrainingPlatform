namespace GarageGenius.Shared.Abstractions.Queries.Query;
public interface IQueryHandler<in TQuery, TQueryResult> where TQuery : IQuery<TQueryResult>
{
	/// <summary>
	/// Handle a query
	/// </summary>
	/// <param name="query">Query</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns></returns>
	// TODO XML comments
	Task<TQueryResult> HandleQueryAsync(TQuery query, CancellationToken cancellationToken = default);
}
