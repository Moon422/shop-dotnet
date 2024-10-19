using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Shop.Net.Core.Domains.Customers;
using Shop.Net.Services.Common;
using Shop.Net.Services.Customers;

namespace Shop.Net.Web.Admin.Middlewares;

public class ThemeToggleMiddleware
{
    private readonly RequestDelegate next;

    public ThemeToggleMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Cookies.TryGetValue("darkMode", out var value) && !string.IsNullOrWhiteSpace(value))
        {
            var darkMode = Convert.ToBoolean(value);
            context.Items["dark-mode"] = darkMode;
        }

        await next(context);
    }
}