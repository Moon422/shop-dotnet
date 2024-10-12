using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Net.Core.Domains;

namespace Shop.Net.Data;

public interface IRepository<T> where T : BaseEntity
{
    Task<IList<T>> GetAllAsync();
    Task<T?> GetOneByIdAsync(int id);

    Task InsertAsync(T entity);

    Task UpdateAsync(T entity);

    Task DeleteAsync(T entity);
}
