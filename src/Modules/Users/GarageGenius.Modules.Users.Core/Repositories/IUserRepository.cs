using GarageGenius.Modules.Users.Core.Entities;
using GarageGenius.Modules.Users.Core.Queries.GetUsers;

namespace GarageGenius.Modules.Users.Core.Repositories;
internal interface IUserRepository
{
	Task<User?> GetAsync(Guid id, CancellationToken cancellationToken = default);
	Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
	Task AddAsync(User user, CancellationToken cancellationToken = default);
	Task UpdateAsync(User user, CancellationToken cancellationToken = default);
	Task DeactivateUserAsync(Guid id, CancellationToken cancellationToken = default);
	Task<IReadOnlyList<GetUsersQueryDto>> GetUsersAsync(CancellationToken cancellationToken = default);
}
