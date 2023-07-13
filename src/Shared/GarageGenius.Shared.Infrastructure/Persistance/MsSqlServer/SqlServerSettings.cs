using Microsoft.Extensions.Options;

namespace GarageGenius.Shared.Infrastructure.Persistance.MsSqlServer;
public class SqlServerSettings
{
    public static string SectionName { get; } = nameof(SqlServerSettings);
    public static Func<SqlServerSettings, bool> ValidationRules => (sqlServerSettings) =>
    {
        if (string.IsNullOrWhiteSpace(sqlServerSettings.SqlServerConnection))
            throw new OptionsValidationException(sqlServerSettings.SqlServerConnection, typeof(string), new[] { "Invalidate SqlServerSettings" });
        return true;
    };

    public string SqlServerConnection { get; set; }
	public bool InMemoryDatabase { get; set; }
}
