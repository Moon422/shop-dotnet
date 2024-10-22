using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Shop.Net.Web.Admin.Factories;
using Shop.Net.Web.Admin.Filters;
using Shop.Net.Web.Admin.Models;
using Shop.Net.Web.Admin.Models.Common;
using Shop.Net.Web.Admin.Models.Customers;

namespace Shop.Net.Web.Admin.Controllers;

public class CustomerController : BaseController
{
    protected readonly ICustomerModelFactory customerModelFactory;

    public CustomerController(ICustomerModelFactory customerModelFactory)
    {
        this.customerModelFactory = customerModelFactory;
    }

    public IActionResult Index()
    {
        return RedirectToAction("List");
    }

    [PermissionAuthorize("customers:list:all, customers:list:read")]
    public async Task<IActionResult> List()
    {
        var searchModel = await customerModelFactory.PrepareCustomerSearchModelAsync(new CustomerSearchModel());

        return View(searchModel);
    }

    [HttpPost]
    [ActionName("List")]
    [PermissionAuthorize("customers:list:all, customers:list:read")]
    public async Task<IActionResult> ListCustomer(CustomerSearchModel searchModel)
    {
        var model = new List<CustomerModel>()
        {
            new CustomerModel()
            {
                Id = 3,
                FirstName = "Mahfuzur",
                LastName = "Rahman",
                Email = "mahfuz@shopdotnet.com",
                PhoneNumber = "+8801748689039"
            },
            new CustomerModel()
            {
                Id = 4,
                FirstName = "Fatima",
                LastName = "Ajwah",
                Email = "ajwah@gmail.com",
                PhoneNumber = "+8801964456592"
            }
        };

        return Json(model, new JsonSerializerOptions()
        {
            PropertyNamingPolicy = null
        });
    }
}