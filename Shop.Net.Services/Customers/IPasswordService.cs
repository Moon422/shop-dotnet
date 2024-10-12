using System.Threading.Tasks;
using Shop.Net.Core.Domains.Customers;

public interface IPasswordService
{
    Task<Password?> GetPasswordByCustomerIdAsync(int customerId);

    Task InsertPasswordAsync(Password entity, bool deferDbInsert = true);

    Task UpdatePasswordAsync(Password entity, bool deferDbUpdate = true);

    Task DeletePasswordAsync(Password entity, bool deferDbDelete = true);
}
