using System.Transactions;

namespace Aton.UserManagement.Dal.Repositories.Interfaces;

public interface IDbRepository
{
    TransactionScope CreateTransactionScope(IsolationLevel level = IsolationLevel.ReadCommitted);
}