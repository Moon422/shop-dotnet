using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Net.Services.Customers;

namespace Shop.Net.Web.Admin.Configurations;

public class AuthController : Controller
{
    protected readonly ICustomerService customerService;

    public AuthController(ICustomerService customerService)
    {
        this.customerService = customerService;
    }

    public async Task<IActionResult> Login(string returnUrl = "")
    {
        return View();
    }
}