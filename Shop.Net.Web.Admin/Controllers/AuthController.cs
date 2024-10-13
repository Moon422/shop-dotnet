using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Net.Services.Customers;
using Shop.Net.Web.Admin.Models;

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
        var model = new LoginModel();
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginModel model)
    {
        return View(model);
    }
}