using GarageGenius.Modules.Users.Application.Queries.GetUser;
using GarageGenius.Modules.Users.Application.Queries.GetUsers;
using GarageGenius.Modules.Users.Core.Entities;

namespace GarageGenius.Modules.Users.Application.ServiceMapper;
internal interface IUserServiceMapper
{
	public GetUserQueryDto MapToGetUserQueryDto(User entity);
	public GetUsersQueryDto MapToGetUsersQueryDto(IEnumerable<User> entities);
}

internal class UserServiceMapper : IUserServiceMapper
{
	public GetUserQueryDto MapToGetUserQueryDto(User entity)
	{
		return new GetUserQueryDto(entity.UserId, entity.CustomerId, entity?.Role?.Name, entity?.Email, entity?.State, entity.Created);
	}

	public GetUsersQueryDto MapToGetUsersQueryDto(IEnumerable<User> entities)
	{
		return new GetUsersQueryDto(entities.Select(user =>
			new GetUsersDto(user.UserId, user.CustomerId, user.Role.Name, user.Email, user.State, user.Created)));
	}
}

// TODO i dont known whether this is the right way to do this,
// maybe better is create a QueryStorage like in Vehicles module (IVehicleQueryStorage)