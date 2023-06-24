namespace GarageGenius.Shared.Abstractions.Authentication.JsonWebToken.Models;

/// <summary>
/// Represent the information about access token, its expiration and subject with claims
/// </summary>
/// <param name="UserId">Subject user identifier</param>
/// <param name="CustomerId">Subject customer identifier</param>
/// <param name="Role">Subject role</param>
/// <param name="AccessToken">Access Token in JSON Web Token</param>
/// <param name="Expiry">Date when the access token expires</param>
/// <param name="Claims">Subject claims attached to access token</param>
public record JsonWebTokenResponse(Guid UserId, Guid CustomerId, string AccessToken, DateTime Expiry, string Role, IDictionary<string, object> Claims);
