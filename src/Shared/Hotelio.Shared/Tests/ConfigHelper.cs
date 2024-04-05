using Hotelio.Shared.SqlServer;
using Microsoft.Extensions.Configuration;

namespace Hotelio.Shared.Tests;

public static class ConfigHelper
{
    private const string AppSettings = "appsettings.json";

    public static SqlServerOptions GetSqlServerConfig() => new SqlServerOptions()
    {
        //TODO load from appsettings
        ConnectionString = "Server=localhost,1433;Database=tests;User=sa;Password=Your_password123;TrustServerCertificate=True"
    };
    
    public static IConfigurationRoot GetConfigurationRoot()
        => new ConfigurationBuilder()
            .AddJsonFile(AppSettings, true)
            .AddEnvironmentVariables()
            .Build();
}