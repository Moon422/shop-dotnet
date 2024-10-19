using System;
using System.Threading.Tasks;
using Shop.Net.Core;

namespace Shop.Net.Data;

[ScopeDependency(typeof(ITransactionManager))]
public class TransactionManager : ITransactionManager
{
    private ShopDbContext shopDbContext;

    public TransactionManager(ShopDbContext shopDbContext)
    {
        this.shopDbContext = shopDbContext;
    }

    public async Task RunTransactionAsync(Func<Task> transactionOperation)
    {
        using var transaction = await shopDbContext.Database.BeginTransactionAsync();

        try
        {
            await transactionOperation();
            await shopDbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
        }
    }
}