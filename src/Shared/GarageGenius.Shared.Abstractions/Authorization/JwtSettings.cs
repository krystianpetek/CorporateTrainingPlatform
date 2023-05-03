using Microsoft.Extensions.Configuration;

namespace GarageGenius.Shared.Abstractions.Authorization;
public class JwtSettings : IJwtSettings
{
    private readonly IConfiguration _configuration;

    public JwtSettings(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string TokenKey => _configuration["JwtSecret"];
}
