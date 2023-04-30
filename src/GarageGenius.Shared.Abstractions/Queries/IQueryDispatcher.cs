namespace GarageGenius.Shared.Abstractions.Queries;
public interface IQueryDispatcher
{
    Task<TQueryResult> QueryAsync<TQueryResult>(IQuery<TQueryResult> query, CancellationToken cancellationToken = default);
}
