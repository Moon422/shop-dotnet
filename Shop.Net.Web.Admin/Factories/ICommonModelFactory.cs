using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Shop.Net.Web.Admin.Models.Common;

namespace Shop.Net.Web.Admin.Factories;

public interface ICommonModelFactory
{
    Task<SidebarRootModel> PrepareSidebarNodeModelAsync(SidebarRootModel rootModel);
}

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
            rootModel = new SidebarRootModel();

        rootModel.ChildrenNodes.Add(new SidebarNodeModel()
        {
            SystemName = "dashboard",
            Text = "Dashboard",
            Url = urlHelper.Action("Index", "Home")
        });

        var customerNode = new SidebarNodeModel()
        {
            SystemName = "customers",
            Text = "Customers",
            Url = urlHelper.Action("List", "Customer"),
        };

        customerNode.ChildrenNodes.Add(new SidebarNodeModel()
        {
            SystemName = "customer-customerroles",
            Text = "Customer Roles",
            Url = urlHelper.Action("List", "CustomerRole")
        });

        rootModel.ChildrenNodes.Add(customerNode);

        return rootModel;
    }
}