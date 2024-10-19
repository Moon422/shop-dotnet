using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Net.Core.Settings;
using Shop.Net.Web.Admin.Attributes;
using Shop.Net.Web.Admin.Factories;
using Shop.Net.Web.Admin.Models.Settings;

namespace Shop.Net.Web.Admin.Controllers;

[Authorize]
public class SettingsController : Controller
{
    protected readonly CustomerSettings customerSettings;
    protected readonly IMapper mapper;
    protected readonly ISettingsModelFactory settingsModelFactory;

    public SettingsController(CustomerSettings customerSettings,
        IMapper mapper,
        ISettingsModelFactory settingsModelFactory)
    {
        this.customerSettings = customerSettings;
        this.mapper = mapper;
        this.settingsModelFactory = settingsModelFactory;
    }

    [PermissionAuthorize("settings:customers:all, settings:customers:view, settings:customers:edit")]
    public async Task<IActionResult> Customer()
    {
        var model = mapper.Map<CustomerSettingsModel>(customerSettings);
        model = await settingsModelFactory.PrepareCustomerSettingsModelAsync(model, customerSettings);

        return View(model);
    }
}