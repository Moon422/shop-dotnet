using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Net.Core.Configurations;
using Shop.Net.Core.Domains.Customers;
using Shop.Net.Data;

public class PasswordService : IPasswordService
{
    protected readonly IRepository<Password> passwordRepository;
    protected readonly IApplicationSettings applicationSettings;

    public PasswordService(IRepository<Password> passwordRepository,
        IApplicationSettings applicationSettings)
    {
        this.passwordRepository = passwordRepository;
        this.applicationSettings = applicationSettings;
    }

    public async Task<Password?> GetPasswordByCustomerIdAsync(int customerId)
    {
        return customerId > 0 ?
            await passwordRepository.Table
                .FirstOrDefaultAsync(p => p.CustomerId == customerId) :
            throw new ArgumentException($"{nameof(customerId)} cannot be zero or less than zero");
    }

    public async Task InsertPasswordAsync(Password entity, bool deferDbInsert = true)
    {
        ArgumentNullException.ThrowIfNull(entity);

        await passwordRepository.InsertAsync(entity, deferDbInsert);
    }

    public async Task UpdatePasswordAsync(Password entity, bool deferDbUpdate = true)
    {
        ArgumentNullException.ThrowIfNull(entity);

        await passwordRepository.UpdateAsync(entity, deferDbUpdate);
    }

    public async Task DeletePasswordAsync(Password entity, bool deferDbDelete = true)
    {
        ArgumentNullException.ThrowIfNull(entity);

        await passwordRepository.DeleteAsync(entity, deferDbDelete);
    }
}