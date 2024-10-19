using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Net.Core.Domains.Customers;
using Shop.Net.Services.Common;
using Shop.Net.Services.Customers;

namespace Shop.Net.Web.Admin.Controllers;

public class CommonController : Controller
{
    protected readonly IWorkContext workContext;
    protected readonly ICustomerService customerService;

    public CommonController(IWorkContext workContext,
        ICustomerService customerService)
    {
        this.workContext = workContext;
        this.customerService = customerService;
    }

    public async Task<IActionResult> SwitchTheme(IFormCollection form)
    {
        if (form.TryGetValue("dark-model", out var value))
        {
            var darkModeEnabled = Convert.ToBoolean(value);
            if ((await workContext.GetActiveCustomerAsync()) is Customer customer)
            {
                customer.Theme = darkModeEnabled ? "dark" : "light";
                await customerService.UpdateCustomerAsync(customer);

                return Ok();
            }
        }

        return BadRequest();
    }
}