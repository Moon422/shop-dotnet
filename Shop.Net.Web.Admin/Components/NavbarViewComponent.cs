using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Shop.Net.Web.Admin.Components;

public class NavbarViewComponent : ViewComponent
{
    private readonly IWorkContext workContext;
    private readonly IMapper mapper;

    public NavbarViewComponent(IWorkContext workContext, IMapper mapper)
    {
        this.workContext = workContext;
        this.mapper = mapper;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var model = new NavbarModel();

        var profile = await workContext.GetCurrentLoggedInProfileAsync();
        if (profile is null)
        {
            return View(model);
        }

        model.Profile = mapper.Map<ReadProfileModel>(profile);

        return View(model);
    }
}