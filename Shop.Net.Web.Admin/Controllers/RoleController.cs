using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Net.Web.Admin.Factories;
using Shop.Net.Web.Admin.Filters;
using Shop.Net.Web.Admin.Models;

namespace Shop.Net.Web.Admin.Controllers;

[Authorize]
public class RoleController : Controller
{
    protected readonly IRoleModelFactory roleModelFactory;

    public RoleController(IRoleModelFactory roleModelFactory)
    {
        this.roleModelFactory = roleModelFactory;
    }

    public IActionResult Index()
    {
        return RedirectToAction("List");
    }

    [PermissionAuthorize("roles:all, roles:view, roles:edit, roles.add, roles.remove")]
    public async Task<IActionResult> List()
    {
        var model = await roleModelFactory.PrepareRoleSearchModelAsync(new RoleSearchModel());
        return View(model);
    }

    // [HttpPost]
    // [ValidateAntiForgeryToken]
    // [PermissionAuthorize("roles:all, roles:view, roles:edit, roles.add, roles.remove")]
    // public async Task<IActionResult> RoleList(RoleSearchModel roleSearchModel)
    // {

    // }
}