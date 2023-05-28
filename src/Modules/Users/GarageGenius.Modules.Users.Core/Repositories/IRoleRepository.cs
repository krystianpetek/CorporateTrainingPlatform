using GarageGenius.Modules.Users.Core.Entities;

namespace GarageGenius.Modules.Users.Core.Repositories;
internal interface IRoleRepository
{
	Task<Role?> GetAsync(string name, CancellationToken cancellationToken = default);
	Task<IReadOnlyList<Role>> GetRolesAsync(CancellationToken cancellationToken = default);
	Task AddAsync(Role role, CancellationToken cancellationToken = default);
}
