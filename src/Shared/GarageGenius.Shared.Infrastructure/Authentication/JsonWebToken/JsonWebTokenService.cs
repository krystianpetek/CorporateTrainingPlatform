using GarageGenius.Shared.Abstractions.Authentication.JsonWebToken;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace GarageGenius.Shared.Infrastructure.Authentication.JsonWebToken;
internal class JsonWebTokenService : IJsonWebTokenService
{
    private readonly JsonWebTokenOptions _jsonWebTokenOptions;
    public JsonWebTokenService(IOptions<JsonWebTokenOptions> jsonWebTokenOptions)
    {
        _jsonWebTokenOptions = jsonWebTokenOptions.Value;
    }

    public string GenerateToken(string username, string role = null, IDictionary<string, IEnumerable<string>> claims = null, string audience = null)
    {
        byte[]? symmetricKey = Convert.FromBase64String("qwertyuiopasdfghjklzxcvbnm987123");
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

        var customClaims = new List<Claim>();
        foreach (var (claim, values) in claims)
        {
            customClaims.AddRange(values.Select(value => new Claim(claim, value)));
        }

        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
                        {
                                new Claim(ClaimTypes.Name, username)
                            }),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
        };

        SecurityToken stoken = tokenHandler.CreateToken(tokenDescriptor);
        string token = tokenHandler.WriteToken(stoken);

        return token;
    }
}
