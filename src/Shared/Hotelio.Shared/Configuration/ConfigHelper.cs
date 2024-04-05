using Hotelio.Shared.SqlServer;
using Microsoft.Extensions.Configuration;

namespace Hotelio.Shared.Configuration;

public static class ConfigHelper
{
    public static SqlServerOptions GetSqlServerConfig(IConfiguration configuration)
    {
        var options = new SqlServerOptions();
        configuration.GetSection("SqlServer").Bind(options);

        return options;
    } 
}