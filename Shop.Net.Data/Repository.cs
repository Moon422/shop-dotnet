using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Net.Core.Domains;

namespace Shop.Net.Data;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly ShopDbContext shopDbContext;

    public readonly DbSet<T> Table;

    public Repository(ShopDbContext dbContext)
    {
        Table = dbContext.Set<T>();
    }

    public async Task<IList<T>> GetAllAsync()
    {
        return await Table.ToListAsync();
    }

    public async Task<T?> GetOneByIdAsync(int id)
    {
        return await Table.FindAsync(id);
    }

    public async Task InsertAsync(T entity, bool insertImmediately = true)
    {
        if (!insertImmediately)
        {
            await Table.AddAsync(entity);
            return;
        }

        using var transaction = await shopDbContext.Database.BeginTransactionAsync();

        try
        {
            await Table.AddAsync(entity);
            await shopDbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
        }
    }

    public async Task UpdateAsync(T entity, bool updateImmediately = true)
    {
        if (!updateImmediately)
        {
            Table.Update(entity);
            return;
        }

        using var transaction = await shopDbContext.Database.BeginTransactionAsync();

        try
        {
            Table.Update(entity);
            await shopDbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
        }
    }

    public async Task DeleteAsync(T entity, bool deleteImmediately = true)
    {
        if (!deleteImmediately)
        {
            Table.Remove(entity);
            return;
        }

        using var transaction = await shopDbContext.Database.BeginTransactionAsync();

        try
        {
            Table.Remove(entity);
            await shopDbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
        }
    }
}