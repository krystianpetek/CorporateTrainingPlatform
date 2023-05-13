using Microsoft.Extensions.Options;

namespace GarageGenius.Shared.Abstractions.Authentication.JsonWebToken.Models;

/// <summary>
/// JSON Web Token options class
/// </summary>
public class JsonWebTokenOptions
{
    /// <summary>
    /// Section name key in appsettings.json
    /// </summary>
    public static string SectionName { get; } = nameof(JsonWebTokenOptions);

    /// <summary>
    /// Issuer - claim identifies the principal that issued the JWT
    /// </summary>
    public string Issuer { get; init; }
    /// <summary>
    /// Audience - claim identifies the principal that is the subject of the JWT.
    /// </summary>
    public string Audience { get; init; }
    /// <summary>
    /// Expires - claim identifies the expiration time on or after which the JWT MUST NOT be accepted for processing.
    /// </summary>
    public TimeSpan Expiration { get; init; }
    /// <summary>
    /// Issuer signing key - a cryptographic key that is used by an identity provider to sign the tokens that it issues.
    /// </summary>
    public string IssuerSigningKey { get; init; }

    /// <summary>
    /// A flag indicating whether to validate the issuer claim.
    /// </summary>
    public bool ValidateIssuer { get; init; }
    /// <summary>
    /// A flag indicating whether to validate the audience claim.
    /// </summary>
    public bool ValidateAudience { get; init; }
    /// <summary>
    /// A flag indicating whether to validate the expiration and not before claims.
    /// </summary>
    public bool ValidateLifetime { get; init; }
    /// <summary>
    /// A flag indicating whether to validate the signing key used to sign the JWT.
    /// </summary>
    public bool ValidateIssuerSigningKey { get; init; }

    public static Func<JsonWebTokenOptions, bool> ValidationRules => (jsonWebTokenOptions) =>
    {
        if (string.IsNullOrWhiteSpace(jsonWebTokenOptions.IssuerSigningKey) || jsonWebTokenOptions.IssuerSigningKey.Length < 60)
            throw new OptionsValidationException(jsonWebTokenOptions.IssuerSigningKey, typeof(string), new[] { "Invalidate IssuerSigningKey" });
        // TODO settings validation rules
        return true;
    };
}
