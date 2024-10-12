using System;
using System.Threading.Tasks;

namespace Shop.Net.Data;

public interface ITransactionManager
{
    Task RunTransactionAsync(Func<Task> transactionQuery);
}
