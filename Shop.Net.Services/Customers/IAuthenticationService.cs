using System.Threading.Tasks;
using Shop.Net.Core.Domains.Customers;

namespace Shop.Net.Services.Customers;

public interface IAuthenticationService
{
    Task<Customer> RegisterCustomerAsync(Customer customer, Password password);

    Task<Customer?> LoginCustomerAsync(string email, string password);

    Task<bool> GeneratePasswordResetTokenAsync(string email);

    Task InvalidateResetTokenAsync(ResetPasswordRequest resetPasswordRequest);

    Task<ResetPasswordRequest?> VerifyResetTokenAsync(string otp, bool ignoreTimeout = false);
}
