using GarageGenius.Modules.Users.Core.Entities;
using GarageGenius.Modules.Users.Core.Queries.GetUser;

namespace GarageGenius.Modules.Users.Core.ServiceMapper;
internal interface IUserServiceMapper
{
	public GetUserQueryDto MapToGetUserQueryDto(User entity);
}

internal class UserServiceMapper : IUserServiceMapper
{
	public GetUserQueryDto MapToGetUserQueryDto(User entity)
	{
		return new GetUserQueryDto(entity.UserId, entity.CustomerId, entity?.Role?.Name, entity?.Email, entity?.State, entity.Created);
	}
}

// TODO i dont known whether this is the right way to do this,
// maybe better is create a QueryStorage like in Vehicles module (IVehicleQueryStorage)