using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Shop.Net.Core.Domains.Customers;
using Shop.Net.Services.Customers;
using Shop.Net.Web.Admin.Factories;
using Shop.Net.Web.Admin.Models;

namespace Shop.Net.Web.Admin.Configurations;

public class AuthController : Controller
{
    protected readonly ICustomerService customerService;
    protected readonly IAuthModelFactory authModelFactory;

    public AuthController(ICustomerService customerService,
        IAuthModelFactory authModelFactory)
    {
        this.customerService = customerService;
        this.authModelFactory = authModelFactory;
    }

    #region Utilities

    private async Task SignInAsync(Customer customer, bool isPersistent)
    {
        ArgumentNullException.ThrowIfNull(customer);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, customer.Email)
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = isPersistent
        };

        await HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity), authProperties);
    }

    #endregion


    public async Task<IActionResult> Login(string returnUrl = "")
    {
        var model = authModelFactory.PrepareLoginModelAsync(new LoginModel());
        return View(model);
    }

    [HttpPost]
    [ActionName("Login")]
    public async Task<IActionResult> LoginRequest(LoginModel model, string returnUrl = "")
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var customer = await customerService.LoginCustomerAsync(model.Email, model.Password);
        if (customer is null)
        {
            ModelState.AddModelError(ErrorKeys.GlobalViewErrorKey, "Invalid Credentials");
            return View(model);
        }

        await SignInAsync(customer, model.IsPersistent);


    }
}