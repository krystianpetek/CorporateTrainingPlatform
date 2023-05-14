using GarageGenius.Shared.Abstractions.Authentication.JsonWebToken.Models;
using System.Security.Claims;

namespace GarageGenius.Shared.Abstractions.Authentication.JsonWebToken;

public interface IJsonWebTokenService
{
    JsonWebTokenResponse GenerateToken(Guid userId, string email, string role, IDictionary<string, object> claims);
    ClaimsPrincipal GetPrincipalFromToken(string token);
}
