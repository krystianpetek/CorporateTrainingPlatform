using Microsoft.Extensions.Options;

namespace GarageGenius.Shared.Infrastructure.Persistance.PostgreSql;
public class PostgreSqlSettings
{
    public static string SectionName { get; } = nameof(PostgreSqlSettings);
    public static Func<PostgreSqlSettings, bool> ValidationRules => (postgreSqlSettings) =>
    {
        if (string.IsNullOrWhiteSpace(postgreSqlSettings.PostgreSqlConnection))
            throw new OptionsValidationException(postgreSqlSettings.PostgreSqlConnection, typeof(string), new[] { "Invalidate PostgreSqlSettings" });
        return true;
    };

    public string PostgreSqlConnection { get; set; }
	public bool InMemoryDatabase { get; set; }
}
