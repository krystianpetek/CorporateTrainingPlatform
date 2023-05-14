using GarageGenius.Shared.Abstractions.Authentication.JsonWebToken;
using GarageGenius.Shared.Abstractions.Authentication.JsonWebToken.Models;
using GarageGenius.Shared.Abstractions.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GarageGenius.Shared.Infrastructure.Authentication.JsonWebToken;
internal class JsonWebTokenService : IJsonWebTokenService
{
    private readonly Serilog.ILogger _logger;
    private readonly ISystemDateService _systemDateService;
    private readonly JsonWebTokenOptions _jsonWebTokenOptions;

    public JsonWebTokenService(
        Serilog.ILogger logger,
        IOptions<JsonWebTokenOptions> jsonWebTokenOptions,
        ISystemDateService systemDateService)
    {
        _logger = logger;
        _jsonWebTokenOptions = jsonWebTokenOptions.Value;
        _systemDateService = systemDateService;
    }

    public JsonWebTokenResponse GenerateToken(Guid userId, string email, string role, IDictionary<string, object> claims)
    {
        SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jsonWebTokenOptions.IssuerSigningKey));
        SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
        DateTime operationDate = _systemDateService.GetCurrentDate();

        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
        {
            Audience = _jsonWebTokenOptions.Audience,
            Issuer = _jsonWebTokenOptions.Issuer,
            Claims = claims,
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, $"{userId}"),
                new Claim(JwtRegisteredClaimNames.UniqueName, $"{userId}"),
                new Claim(JwtRegisteredClaimNames.Jti, $"{Guid.NewGuid()}"),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(ClaimTypes.Role, role),
            }),
            NotBefore = operationDate,
            IssuedAt = operationDate,
            Expires = operationDate.Add(_jsonWebTokenOptions.Expiration),
            SigningCredentials = signingCredentials
        };

        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
        string accessToken = tokenHandler.WriteToken(securityToken);

        _logger.Information("Successfully created access token for user {userId}", userId);

        return new JsonWebTokenResponse(userId, accessToken, operationDate, claims);
        // TODO save token after generate
    }

    public ClaimsPrincipal GetPrincipalFromToken(string token)
    {
        throw new NotImplementedException();

        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jsonWebTokenOptions.IssuerSigningKey));
        SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

        TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = _jsonWebTokenOptions.Issuer,
            ValidAudience = _jsonWebTokenOptions.Audience,
            IssuerSigningKey = signingCredentials.Key,

            ValidateIssuer = _jsonWebTokenOptions.ValidateIssuer,
            ValidateAudience = _jsonWebTokenOptions.ValidateAudience,
            ValidateLifetime = _jsonWebTokenOptions.ValidateLifetime,
            ValidateIssuerSigningKey = _jsonWebTokenOptions.ValidateIssuerSigningKey,
        };
    }
}
