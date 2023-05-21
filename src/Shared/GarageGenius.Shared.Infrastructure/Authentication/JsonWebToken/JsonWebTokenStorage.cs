using GarageGenius.Shared.Abstractions.Authentication.JsonWebToken;
using GarageGenius.Shared.Abstractions.Authentication.JsonWebToken.Models;
using Microsoft.AspNetCore.Http;

namespace GarageGenius.Shared.Infrastructure.Authentication.JsonWebToken;
internal class JsonWebTokenStorage : IJsonWebTokenStorage
{
    private static string SectionName => "jwt";

    private readonly IHttpContextAccessor _httpContextAccessor;

    public JsonWebTokenStorage(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public JsonWebTokenResponse? GetToken()
    {
        if (_httpContextAccessor.HttpContext is null)
            return null;

        if (_httpContextAccessor.HttpContext.Items.TryGetValue(SectionName, out var token))
            return token as JsonWebTokenResponse;

        return null;
    }

    public void RemoveToken()
    {
        _httpContextAccessor?.HttpContext?.Items.Remove(SectionName);
    }

    public void SetToken(JsonWebTokenResponse token)
    {
        _httpContextAccessor?.HttpContext?.Items.TryAdd(SectionName, token);
    }

    // TODO store token in database, maybe sql and redis/distributed cache
}
