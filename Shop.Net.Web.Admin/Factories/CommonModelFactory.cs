using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Newtonsoft.Json;
using Shop.Net.Core;
using Shop.Net.Web.Admin.Models.Common;

namespace Shop.Net.Web.Admin.Factories;

[ScopeDependency(typeof(ICommonModelFactory))]
public class CommonModelFactory : ICommonModelFactory
{
    protected readonly IUrlHelper urlHelper;

    public CommonModelFactory(IUrlHelperFactory urlHelperFactory,
        IActionContextAccessor actionContextAccessor)
    {
        var actionContext = actionContextAccessor.ActionContext;
        urlHelper = actionContext is not null ? urlHelperFactory.GetUrlHelper(actionContext) :
            throw new Exception();
    }

    public async Task<SidebarRootModel> PrepareSidebarNodeModelAsync(SidebarRootModel rootModel)
    {
        if (rootModel is null)
        {
            rootModel = new SidebarRootModel();
        }

        var fileName = "SidebarNodes.json";
        if (!File.Exists(fileName))
        {
            return rootModel;
        }

        var sidebarContent = await File.ReadAllTextAsync(fileName);
        var sidebarNodes = JsonConvert.DeserializeObject<List<SidebarNodeModel>>(sidebarContent) ?? new List<SidebarNodeModel>();

        foreach (var sidebarNode in sidebarNodes)
        {
            rootModel.ChildrenNodes.Add(sidebarNode);
        }

        return rootModel;
    }
}