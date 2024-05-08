using Microsoft.Extensions.Options;
using Npgsql;
using SipCallback.Options;

namespace SipCallback.Database;

public class DbConnectionProvider
{
    public DbConnectionProvider(IOptions<DatabaseOptions> options)
    {
        ConnectionString = BuildConnectionString(options.Value);
        Connection = new NpgsqlConnection(ConnectionString);
    }

    private string ConnectionString { get; }
    private NpgsqlConnection Connection { get; }

    public NpgsqlConnection GetConnection()
    {
        return Connection;
    }

    private static string BuildConnectionString(DatabaseOptions op)
    {
        return $"Server={op.Server};Port={op.Port};Database={op.DbName};User Id={op.Username};Password={op.Password};";
    }
}