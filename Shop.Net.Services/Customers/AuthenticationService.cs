using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Net.Core.Domains.Customers;
using Shop.Net.Data;
using Shop.Net.Services.Caching;

namespace Shop.Net.Services.Customers;

public class AuthenticationService : IAuthenticationService
{
    protected readonly IRepository<ResetPasswordRequest> resetPasswordRequestRepository;
    protected readonly ITransactionManager transactionManager;
    protected readonly ICacheService cacheService;
    protected readonly ICustomerService customerService;
    protected readonly IPasswordService passwordService;

    public AuthenticationService(ITransactionManager transactionManager,
        IRepository<ResetPasswordRequest> resetPasswordRequestRepository,
        ICacheService cacheService,
        ICustomerService customerService,
        IPasswordService passwordService)
    {
        this.transactionManager = transactionManager;
        this.resetPasswordRequestRepository = resetPasswordRequestRepository;
        this.cacheService = cacheService;
        this.passwordService = passwordService;
        this.customerService = customerService;
    }

    protected string GenerateOTP(int length = 6)
    {
        const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        char[] otp = new char[length];
        byte[] data = new byte[length];

        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(data);
        }

        for (int i = 0; i < otp.Length; i++)
        {
            otp[i] = characters[data[i] % characters.Length];
        }

        return new string(otp);
    }


    public async Task<Customer> RegisterCustomerAsync(Customer customer, Password password)
    {
        ArgumentNullException.ThrowIfNull(customer);
        ArgumentNullException.ThrowIfNull(password);

        await transactionManager.RunTransactionAsync(async () =>
        {
            await customerService.InsertCustomerAsync(customer, true, true);

            password.Customer = customer;
            await passwordService.InsertPasswordAsync(password, true, true);
        });

        cacheService.RemoveByPrefix(CustomerCacheKeys.CUSTOMER_PREFIX);
        return customer;
    }

    public async Task<Customer?> LoginCustomerAsync(string email, string password)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(email);
        ArgumentException.ThrowIfNullOrWhiteSpace(password);

        var customer = await customerService.GetCustomerByEmailAsync(email);
        if (customer is null)
        {
            return null;
        }

        var passwordEntity = await passwordService.GetPasswordByCustomerIdAsync(customer.Id);
        if (passwordEntity is null)
        {
            return null;
        }

        if (!passwordService.VerifyPassword(password, passwordEntity.PasswordHash))
        {
            return null;
        }

        return customer;
    }

    public async Task<bool> GeneratePasswordResetTokenAsync(string email)
    {
        var customer = await customerService.GetCustomerByEmailAsync(email);
        if (customer is null)
        {
            return false;
        }

        var resetPasswordRequest = new ResetPasswordRequest
        {
            CustomerId = customer.Id,
            OtpCode = GenerateOTP(),
            IsActive = true,
            ValidTill = DateTime.UtcNow.AddMinutes(5)
        };

        await resetPasswordRequestRepository.InsertAsync(resetPasswordRequest);
        return true;
    }

    public async Task InvalidateResetTokenAsync(ResetPasswordRequest resetPasswordRequest)
    {
        resetPasswordRequest.IsActive = false;
        await resetPasswordRequestRepository.UpdateAsync(resetPasswordRequest);
    }

    public async Task<ResetPasswordRequest?> VerifyResetTokenAsync(string otp, bool ignoreTimeout = false)
    {
        var resetPasswordRequest = await resetPasswordRequestRepository.Table.FirstOrDefaultAsync(rpr => rpr.OtpCode == otp);
        if (!ignoreTimeout && (resetPasswordRequest is null || DateTime.UtcNow > resetPasswordRequest.ValidTill))
        {
            return null;
        }

        return resetPasswordRequest;
    }
}