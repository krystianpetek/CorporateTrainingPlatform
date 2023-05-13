using GarageGenius.Shared.Abstractions.Authentication.JsonWebToken.Models;

namespace GarageGenius.Shared.Abstractions.Authentication.JsonWebToken;
public interface IJsonWebTokenStorage
{
    JsonWebTokenResponse? GetToken();
    void SetToken(JsonWebTokenResponse token);
    void RemoveToken();
}
