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


}