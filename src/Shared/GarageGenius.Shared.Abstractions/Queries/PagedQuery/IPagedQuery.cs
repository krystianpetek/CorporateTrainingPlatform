namespace GarageGenius.Shared.Abstractions.Queries.PagedQuery;

public interface IPagedQuery { }
public interface IPagedQuery<T> : IPagedQuery
{
	int PageNumber { get; init; }
	int PageSize { get; init; }

	//public string OrderBy { get; set; }
	//public string SortOrder { get; set; }
}