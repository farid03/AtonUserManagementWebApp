using System.Transactions;
using Aton.UserManagement.Dal.Repositories.Interfaces;
using Aton.UserManagement.Dal.Settings;
using Npgsql;

namespace Aton.UserManagement.Dal.Repositories;

public abstract class BaseRepository : IDbRepository
{
    private readonly DalOptions _dalSettings;

    protected BaseRepository(DalOptions dalSettings)
    {
        _dalSettings = dalSettings;
    }

    public TransactionScope CreateTransactionScope(
        IsolationLevel level = IsolationLevel.ReadCommitted)
    {
        return new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions
            {
                IsolationLevel = level,
                Timeout = TimeSpan.FromSeconds(5)
            },
            TransactionScopeAsyncFlowOption.Enabled);
    }

    protected async Task<NpgsqlConnection> GetAndOpenConnection()
    {
        var connection = new NpgsqlConnection(_dalSettings.ConnectionString);
        await connection.OpenAsync();
        connection.ReloadTypes();
        return connection;
    }
}