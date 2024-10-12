using System.Threading.Tasks;
using Shop.Net.Core.Domains.Customers;

public interface IPasswordService
{
    string HashPassword(string rawPassword);

    bool VerifyPassword(string rawPassword, string hashedPassword);

    Task<Password?> GetPasswordByCustomerIdAsync(int customerId);

    Task InsertPasswordAsync(Password entity, bool deferDbInsert = true);

    Task UpdatePasswordAsync(Password entity, bool deferDbUpdate = true);

    Task DeletePasswordAsync(Password entity, bool deferDbDelete = true);
}
