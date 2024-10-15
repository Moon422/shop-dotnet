using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shop.Net.Services.Common;
using Shop.Net.Web.Admin.Models;

namespace Shop.Net.Web.Admin.Components;

public class NavbarViewComponent : ViewComponent
{
    private readonly IWorkContext workContext;
    private readonly IMapper mapper;

    public NavbarViewComponent(IWorkContext workContext,
    IMapper mapper)
    {
        this.workContext = workContext;
        this.mapper = mapper;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var model = new NavbarModel();

        var customer = await workContext.GetActiveCustomerAsync();
        if (customer is null)
        {
            return View(model);
        }

        model.Customer = mapper.Map<CustomerModel>(customer);

        return View(model);
    }
}