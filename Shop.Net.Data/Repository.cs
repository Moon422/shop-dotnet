using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Net.Core.Domains;

namespace Shop.Net.Data;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    public DbSet<T> Table { get; }

    public Repository(ShopDbContext dbContext)
    {
        Table = dbContext.Set<T>();
    }

    public Task<IList<T>> GetAllAsync()
    {
        throw new System.NotImplementedException();
    }

    public Task<T?> GetOneByIdAsync(int id)
    {
        throw new System.NotImplementedException();
    }

    public Task InsertAsync(T entity)
    {
        throw new System.NotImplementedException();
    }

    public Task UpdateAsync(T entity)
    {
        throw new System.NotImplementedException();
    }

    public Task DeleteAsync(T entity)
    {
        throw new System.NotImplementedException();
    }
}