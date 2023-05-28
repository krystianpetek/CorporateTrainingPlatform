namespace GarageGenius.Shared.Abstractions.Services;
public interface ICurrentUserService
{
	string UserId { get; }
	string Role { get; }
	bool IsAuthenticated { get; }
}
