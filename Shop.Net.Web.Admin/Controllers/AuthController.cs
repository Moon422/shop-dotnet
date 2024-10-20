using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Net.Core.Domains.Customers;
using Shop.Net.Core.Settings;
using Shop.Net.Services.Customers;
using Shop.Net.Web.Admin.Factories;
using Shop.Net.Web.Admin.Helpers;
using Shop.Net.Web.Admin.Models.Auth;

namespace Shop.Net.Web.Admin.Controllers;

public class AuthController : Controller
{
    protected readonly ICustomerService customerService;
    protected readonly IPasswordService passwordService;
    protected readonly Net.Services.Customers.IAuthenticationService authenticationService;
    protected readonly IRoleService roleService;
    protected readonly IPermissionService permissionService;
    protected readonly IAuthModelFactory authModelFactory;
    protected readonly CustomerSettings customerSettings;

    public AuthController(ICustomerService customerService,
        IPasswordService passwordService,
        Net.Services.Customers.IAuthenticationService authenticationService,
        IRoleService roleService,
        IPermissionService permissionService,
        IAuthModelFactory authModelFactory,
        CustomerSettings customerSettings)
    {
        this.customerService = customerService;
        this.passwordService = passwordService;
        this.authenticationService = authenticationService;
        this.authModelFactory = authModelFactory;
        this.customerSettings = customerSettings;
        this.roleService = roleService;
        this.permissionService = permissionService;
    }

    private async Task SignInAsync(Customer customer, bool isPersistent)
    {
        ArgumentNullException.ThrowIfNull(customer);

        IList<Role> customerRoles = await roleService.GetCustomerRolesAsync(customer.Id);
        IList<string> customerPermissions = await permissionService.GetCustomerPermissionsAsync(customer.Id);

        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, customer.Email),
            new Claim(CustomClaimTypes.IsPersistent, isPersistent ? "true" : "false")
        };

        foreach (var customerRole in customerRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, customerRole.Name));
        }

        foreach (var customerPermission in customerPermissions)
        {
            claims.Add(new Claim(CustomClaimTypes.Permission, customerPermission));
        }

        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        AuthenticationProperties authProperties = new AuthenticationProperties
        {
            IsPersistent = isPersistent
        };

        await HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity), authProperties);
    }

    public async Task<IActionResult> Login(string returnUrl = "")
    {
        var model = await authModelFactory.PrepareLoginModelAsync(new LoginModel());
        return View(model);
    }

    [HttpPost]
    [ActionName("Login")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LoginRequest(LoginModel model, string returnUrl = "")
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var customer = await authenticationService.LoginCustomerAsync(model.Email, model.Password);
        if (customer is null)
        {
            ModelState.AddModelError(ErrorKeys.GlobalViewErrorKey, "Invalid credentials. Please try again.");
            return View(model);
        }

        await SignInAsync(customer, model.IsPersistent);

        if (string.IsNullOrWhiteSpace(returnUrl))
        {
            return RedirectToAction("Index", "Home");
        }

        return Redirect(returnUrl);
    }

    public async Task<IActionResult> Register(string returnUrl)
    {
        if (!customerSettings.PublicCustomerRegistrationEnabled)
        {
            return StatusCode((int)HttpStatusCode.MethodNotAllowed);
        }

        var model = await authModelFactory.PrepareRegisterModelAsync(new RegisterModel());
        return View(model);
    }

    [HttpPost]
    [ActionName("Register")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RegisterRequest(RegisterModel model, string returnUrl)
    {
        if (!customerSettings.PublicCustomerRegistrationEnabled)
        {
            return StatusCode((int)HttpStatusCode.MethodNotAllowed);
        }

        if (string.IsNullOrWhiteSpace(model.Password) || string.IsNullOrWhiteSpace(model.ConfirmPassword) || !string.Equals(model.Password, model.ConfirmPassword))
        {
            ModelState.AddModelError(nameof(RegisterModel.Password), "Invalid Password");
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var customer = new Customer()
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Gender = (Gender)model.GenderId,
            PhoneNumber = model.PhoneNumber,
            Email = model.Email
        };

        var password = new Password()
        {
            PasswordHash = passwordService.HashPassword(model.Password)
        };

        customer = await authenticationService.RegisterCustomerAsync(customer, password);
        await SignInAsync(customer, false);

        if (string.IsNullOrWhiteSpace(returnUrl))
        {
            return RedirectToAction("Index", "Home");
        }

        return Redirect(returnUrl);
    }

    public IActionResult ResetPassword()
    {
        var model = new ResetPasswordRequestModel();
        return View(model);
    }

    [HttpPost]
    [ActionName("ResetPassword")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ResetPasswordRequest(ResetPasswordRequestModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        if (!await authenticationService.GeneratePasswordResetTokenAsync(model.Email))
        {
            ModelState.AddModelError(ErrorKeys.GlobalViewErrorKey, "No account found.");
            return View(model);
        }

        ViewData.Add("success_message", "Email has been sent for reseting your password.");
        return View(model);
    }

    public async Task<IActionResult> ResetPasswordNew(string otp)
    {
        var resetPasswordRequest = await authenticationService.VerifyResetTokenAsync(otp);
        if (resetPasswordRequest is null)
        {
            var model = new PasswordModel();
            ModelState.AddModelError(ErrorKeys.GlobalViewErrorKey, "Invalid reset token. Please try again.");
            return View(model);
        }

        var passwordModel = new PasswordModel
        {
            CustomerId = resetPasswordRequest.CustomerId
        };

        return View(passwordModel);
    }

    [HttpPost]
    [ActionName("ResetPasswordNew")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ResetPasswordNewRequest(string otp, PasswordModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var resetPasswordRequest = await authenticationService.VerifyResetTokenAsync(otp, true);
        if (resetPasswordRequest is null || !resetPasswordRequest.IsActive || resetPasswordRequest.CustomerId != model.CustomerId)
        {
            ModelState.AddModelError(ErrorKeys.GlobalViewErrorKey, "Invalid reset token. Please try again.");
            return View(model);
        }

        await authenticationService.InvalidateResetTokenAsync(resetPasswordRequest);

        var customer = await customerService.GetCustomerByIdAsync(model.CustomerId);
        if (customer is null)
        {
            ModelState.AddModelError(ErrorKeys.GlobalViewErrorKey, "Invalid reset token. Please try again.");
            return RedirectToAction("ResetPassword");
        }

        var password = await passwordService.GetPasswordByCustomerIdAsync(customer.Id);
        if (password is null)
        {
            password = new Password()
            {
                PasswordHash = passwordService.HashPassword(model.Password),
                CustomerId = customer.Id
            };

            await passwordService.InsertPasswordAsync(password);
        }
        else
        {
            password.PasswordHash = passwordService.HashPassword(model.Password);
            await passwordService.UpdatePasswordAsync(password);
        }

        await SignInAsync(customer, false);

        return RedirectToAction("Index", "Home");
    }

    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("Login");
    }
}