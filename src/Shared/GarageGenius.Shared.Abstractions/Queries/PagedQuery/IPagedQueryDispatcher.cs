namespace GarageGenius.Shared.Abstractions.Queries.PagedQuery;
public interface IPagedQueryDispatcher
{
	Task<TPagedQueryResult> DispatchPagedQueryAsync<TPagedQueryResult>(IPagedQuery<TPagedQueryResult> query, CancellationToken cancellationToken = default);
}
