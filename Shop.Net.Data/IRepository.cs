using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Net.Core.Domains;

namespace Shop.Net.Data;

public interface IRepository<T> where T : BaseEntity
{
    Task<IList<T>> GetAllAsync();
    Task<T?> GetOneByIdAsync(int id);

    Task InsertAsync(T entity, bool insertImmediately = true);

    Task UpdateAsync(T entity, bool updateImmediately = true);

    Task DeleteAsync(T entity, bool deleteImmediately = true);
}
