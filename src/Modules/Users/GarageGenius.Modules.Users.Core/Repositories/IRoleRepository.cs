using GarageGenius.Modules.Users.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace GarageGenius.Modules.Users.Core.Repositories;
internal interface IRoleRepository
{
    Task<Role> GetAsync(string name);
    Task<IReadOnlyList<Role>> GetAllAsync();
    Task AddAsync(Role role);
}
