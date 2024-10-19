using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Shop.Net.Core.Domains;

namespace Shop.Net.Data;

public interface IRepository<T> where T : BaseEntity
{
    IQueryable<T> Table { get; }

    Task<IList<T>> GetAllAsync(bool? sortByIdDesc = null);

    // Task<PagedList<T>> GetAllAsync(int pageIndex = 0, int pageSize = int.MaxValue, bool sortByIdDesc = true);

    Task<T?> GetOneByIdAsync(int id);

    Task InsertAsync(T entity, bool deferInsert = false);

    Task UpdateAsync(T entity, bool deferUpdate = false);

    Task DeleteAsync(T entity, bool deferDelete = false);
}
