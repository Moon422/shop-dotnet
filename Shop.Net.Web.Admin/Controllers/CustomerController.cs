using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Net.Web.Admin.Factories;
using Shop.Net.Web.Admin.Filters;
using Shop.Net.Web.Admin.Models;
using Shop.Net.Web.Admin.Models.Common;

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
        var model = new TableModel()
        {
            Columns = ["ABc", "DEf", "GHi"]
        };

        return View("_Table", model);
    }

    [HttpPost]
    [ActionName("List")]
    [PermissionAuthorize("customers:list:all, customers:list:read")]
    public async Task<IActionResult> ListCustomer()
    {
        var searchModel = await customerModelFactory.PrepareCustomerSearchModelAsync(new CustomerSearchModel());
        return View(searchModel);
    }
}