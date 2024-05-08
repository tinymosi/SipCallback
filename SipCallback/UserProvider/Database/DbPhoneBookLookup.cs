using Dapper;
using Npgsql;
using SipCallback.Database;

namespace SipCallback.UserProvider.Database;

public class DbPhoneBookLookup : IPhoneBookLookup
{
    private readonly NpgsqlConnection _connection;

    public DbPhoneBookLookup(DbConnectionProvider connectionProvider)
    {
        _connection = connectionProvider.GetConnection();
    }

    public async Task<IPhoneBookEntry?> FindAsync(int accountNumber)
    {
        await using (_connection)
        {
            const string query = "SELECT * FROM phone_book WHERE account_number = @AccountNumber";
            var param = new { AccountNumber = accountNumber };

            return await _connection.QueryFirstOrDefaultAsync<PhonebookEntry>(query, param);
        }
    }
}