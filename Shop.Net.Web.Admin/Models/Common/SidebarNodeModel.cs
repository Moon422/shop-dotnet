using System.Collections.Generic;

namespace Shop.Net.Web.Admin.Models.Common;

public record SidebarNodeModel : BaseModel
{
    public string SystemName { get; set; }

    public string Text { get; set; }

    public string? Url { get; set; }

    public string? Icon { get; set; }

    public IList<SidebarNodeModel> ChildrenNodes { get; set; }

    public SidebarNodeModel()
    {
        ChildrenNodes = new List<SidebarNodeModel>();
    }
}

public record SidebarRootModel : BaseModel
{
    public string ActiveSystemName { get; set; }

    public IList<SidebarNodeModel> ChildrenNodes { get; set; }

    public SidebarRootModel()
    {
        ChildrenNodes = new List<SidebarNodeModel>();
    }
}