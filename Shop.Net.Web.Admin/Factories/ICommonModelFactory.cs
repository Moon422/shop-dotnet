using System.Collections;
using System.Threading.Tasks;
using Shop.Net.Web.Admin.Models.Common;

namespace Shop.Net.Web.Admin.Factories;

public interface ICommonModelFactory
{
    Task<SidebarRootModel> PrepareSidebarNodeModelAsync(SidebarRootModel rootModel);
}
