namespace GarageGenius.Shared.Abstractions.Queries.PagedQuery;
public interface IPagedQueryHandler<in TPagedQuery, TPagedQueryResult> where TPagedQuery : IPagedQuery<TPagedQueryResult>
{
	Task<TPagedQueryResult> HandlePagedQueryAsync(TPagedQuery query, CancellationToken cancellationToken = default);
}
