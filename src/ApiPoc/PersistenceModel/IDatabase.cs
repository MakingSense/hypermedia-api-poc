using System.Collections.Generic;
using System.Linq;

namespace ApiPoc.PersistenceModel
{
    public interface IDatabase
    {
        IEnumerable<Account> GetAccounts();
        Account GetCurrentAccount();
        Account GetAccountById(int accountId);
    }
}