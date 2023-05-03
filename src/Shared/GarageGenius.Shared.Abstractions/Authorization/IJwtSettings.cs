using Microsoft.Extensions.Configuration;

namespace GarageGenius.Shared.Abstractions.Authorization;
public interface IJwtSettings
{
    string TokenKey { get; }
}
