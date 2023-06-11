namespace GarageGenius.Shared.Abstractions.Helpers;
public class PaginatedList<T>
{
	public int CurrentPage { get; set; }
	public int ResultsPerPage { get; set; }
	public int TotalPages { get; set; }
	public long TotalResults { get; set; }
	public IReadOnlyList<T> Items { get; set; }

	public PaginatedList(
		IReadOnlyList<T> items,
		int currentPage,
		int resultsPerPage,
		int totalPages,
		long totalResults)
	{
		Items = items;
		CurrentPage = currentPage;
		ResultsPerPage = resultsPerPage;
		TotalPages = totalPages;
		TotalResults = totalResults;
	}
}