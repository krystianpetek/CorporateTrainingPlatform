using System.Security.Claims;

namespace GarageGenius.Shared.Abstractions.Authentication.JsonWebToken;
public interface IJsonWebTokenService
{
    JsonWebTokenDto GenerateToken(Guid userId, string email, string role, IDictionary<string, object> claims);
    ClaimsPrincipal GetPrincipalFromToken(string token);
}
