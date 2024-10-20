using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Net.Web.Admin.Filters;

namespace Shop.Net.Web.Admin.Controllers;

[Authorize]
[PermissionAuthorize("admin")]
public abstract class BaseController : Controller
{

}
