using Microsoft.EntityFrameworkCore;

namespace GarageGenius.Shared.Abstractions.Helpers;
public static class Extensions
{
	public static async Task<PaginatedList<T>> PaginateAsync<T>(this IQueryable<T> data, int pageNumber, int pageSize,
		CancellationToken cancellationToken = default)
	{
		if (pageNumber <= 0)
			pageNumber = 1;

		pageSize = pageSize switch
		{
			<= 0 => 10,
			> 100 => 100,
			_ => pageSize
		};

		var totalResults = await data.CountAsync();
		var totalPages = totalResults <= pageSize ? 1 : (int)Math.Floor((double)totalResults / pageSize);
		var result = await data.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

		return new PaginatedList<T>(result, pageNumber, pageSize, totalPages, totalResults);
	}
}
