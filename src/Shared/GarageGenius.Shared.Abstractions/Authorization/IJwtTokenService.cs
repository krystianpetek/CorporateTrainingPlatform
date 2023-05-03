namespace GarageGenius.Shared.Abstractions.Authorization;
public interface IJwtTokenService
{
    string GenerateToken(string username);
}
