namespace GarageGenius.Shared.Abstractions.Authentication.JsonWebToken;

/// <summary>
/// Represent the information about access token, its expiration and subject with claims
/// </summary>
/// <param name="UserId">Subject identifier</param>
/// <param name="AccessToken">Access Token in JSON Web Token</param>
/// <param name="Expiry">Date when the access token expires</param>
/// <param name="Claims">Subject claims attached to access token</param>
public record JsonWebTokenDto(Guid UserId, string AccessToken, DateTime Expiry, IDictionary<string, object> Claims);
