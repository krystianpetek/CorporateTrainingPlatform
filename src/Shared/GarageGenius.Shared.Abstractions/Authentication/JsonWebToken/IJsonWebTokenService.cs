namespace GarageGenius.Shared.Abstractions.Authentication.JsonWebToken;
public interface IJsonWebTokenService
{
    string GenerateToken(string username, string role = null, IDictionary<string, IEnumerable<string>> claims = null, string audience = null);
}
