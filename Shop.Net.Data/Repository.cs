using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Net.Core.Domains;
using Shop.Net.Core.Domains.Customers;

namespace Shop.Net.Data;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly ShopDbContext shopDbContext;
    private readonly DbSet<T> dbSet;

    public IQueryable<T> Table => dbSet;

    public Repository(ShopDbContext dbContext)
    {
        shopDbContext = dbContext;
        dbSet = dbContext.Set<T>();
    }

    public async Task<IList<T>> GetAllAsync(bool sortByIdDesc = true)
    {
        if (sortByIdDesc)
            return await Table.OrderByDescending(e => e.Id).ToListAsync();

        return await Table.OrderBy(e => e.Id).ToListAsync();
    }

    public async Task<PagedList<T>> GetAllAsync(int pageIndex = 0, int pageSize = int.MaxValue, bool sortByIdDesc = true)
    {
        IQueryable<T> query = dbSet;

        if (sortByIdDesc)
        {
            query = query.OrderByDescending(e => e.Id);
        }
        else
        {
            query = query.OrderBy(e => e.Id);
        }

        var data = await query.Skip(pageIndex)
            .Take(pageSize)
            .ToListAsync();

        return new PagedList<T>
        {
            Data = data,
            PageIndex = pageIndex,
            TotalItemCount = await dbSet.CountAsync(),
            MaximumPageItemCount = pageSize
        };
    }

    public async Task<T?> GetOneByIdAsync(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException($"{nameof(id)} cannot be zero or negative");
        }
        return await dbSet.FindAsync(id);
    }

    public async Task InsertAsync(T entity, bool deferInsert = false)
    {
        ArgumentNullException.ThrowIfNull(entity);

        if (deferInsert)
        {
            await dbSet.AddAsync(entity);
            return;
        }

        using var transaction = await shopDbContext.Database.BeginTransactionAsync();

        try
        {
            await dbSet.AddAsync(entity);
            await shopDbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
        }
    }

    public async Task UpdateAsync(T entity, bool deferUpdate = false)
    {
        ArgumentNullException.ThrowIfNull(entity);

        if (deferUpdate)
        {
            dbSet.Update(entity);
            return;
        }

        using var transaction = await shopDbContext.Database.BeginTransactionAsync();

        try
        {
            dbSet.Update(entity);
            await shopDbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
        }
    }

    public async Task DeleteAsync(T entity, bool deferDelete = false)
    {
        ArgumentNullException.ThrowIfNull(entity);

        if (deferDelete)
        {
            dbSet.Remove(entity);
            return;
        }

        using var transaction = await shopDbContext.Database.BeginTransactionAsync();

        try
        {
            dbSet.Remove(entity);
            await shopDbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
        }
    }
}