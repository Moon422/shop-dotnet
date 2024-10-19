using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Shop.Net.Core;
using Shop.Net.Core.Domains.Customers;
using Shop.Net.Services.Common;
using Shop.Net.Services.Customers;

namespace Shop.Net.Web.Admin.Services;

[ScopeDependency(typeof(IWorkContext))]
public class WorkContext : IWorkContext
{
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly ICustomerService customerService;

    public WorkContext(IHttpContextAccessor httpContextAccessor, ICustomerService customerService)
    {
        this.httpContextAccessor = httpContextAccessor;
        this.customerService = customerService;
    }

    public async Task<Customer?> GetActiveCustomerAsync()
    {
        if (!httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? true)
        {
            return null;
        }

        var email = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value ?? "";

        if (string.IsNullOrWhiteSpace(email))
        {
            return null;
        }

        var customer = await customerService.GetCustomerByEmailAsync(email);
        return customer;
    }
}