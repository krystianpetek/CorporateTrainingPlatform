using GarageGenius.Shared.Abstractions.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime;
using System.Security.Claims;

namespace GarageGenius.Shared.Infrastructure.Authorization;
internal class JwtTokenService : IJwtTokenService
{
    private readonly IJwtSettings _jwtSettings;
    public JwtTokenService(IJwtSettings jwtSettings)
    {
        _jwtSettings = jwtSettings;
    }

    public string GenerateToken(string username)
    {
        var symmetricKey = Convert.FromBase64String(_jwtSettings.TokenKey);
        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
                        {
                                new Claim(ClaimTypes.Name, username)
                            }),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
        };

        var stoken = tokenHandler.CreateToken(tokenDescriptor);
        var token = tokenHandler.WriteToken(stoken);

        return token;
    }
}
