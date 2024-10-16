using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Net.Web.Admin.Factories;
using Shop.Net.Web.Admin.Models.Common;

namespace Shop.Net.Web.Admin.Components;

public class SidebarViewComponent : ViewComponent
{
    protected readonly ICommonModelFactory commonModelFactory;

    public SidebarViewComponent(ICommonModelFactory commonModelFactory)
    {
        this.commonModelFactory = commonModelFactory;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var model = await commonModelFactory.PrepareSidebarNodeModelAsync(new SidebarRootModel());
        return View(model);
    }
}