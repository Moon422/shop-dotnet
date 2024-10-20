using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shop.Net.Core.Settings;
using Shop.Net.Services.Common;
using Shop.Net.Web.Admin.Factories;
using Shop.Net.Web.Admin.Filters;
using Shop.Net.Web.Admin.Models.Settings;

namespace Shop.Net.Web.Admin.Controllers;

public class SettingsController : BaseController
{
    protected readonly CustomerSettings customerSettings;
    protected readonly ISettingsService settingsService;
    protected readonly IMapper mapper;
    protected readonly ISettingsModelFactory settingsModelFactory;

    public SettingsController(CustomerSettings customerSettings,
        ISettingsService settingsService,
        IMapper mapper,
        ISettingsModelFactory settingsModelFactory)
    {
        this.customerSettings = customerSettings;
        this.mapper = mapper;
        this.settingsModelFactory = settingsModelFactory;
        this.settingsService = settingsService;
    }

    [PermissionAuthorize("settings:customers:all, settings:customers:read, settings:customers:update")]
    public async Task<IActionResult> Customer()
    {
        var model = mapper.Map<CustomerSettingsModel>(customerSettings);
        model = await settingsModelFactory.PrepareCustomerSettingsModelAsync(model, customerSettings);

        return View(model);
    }

    [HttpPost]
    [ActionName("Customer")]
    [ValidateAntiForgeryToken]
    [PermissionAuthorize("settings:customers:all, settings:customers:update")]
    public async Task<IActionResult> SaveCustomerSettings(CustomerSettingsModel model)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("error-message", "Settings failed to save");
            return View(model);
        }

        var settings = mapper.Map<CustomerSettings>(model);
        await settingsService.SaveSettingsAsync(settings);

        ViewData["success-message"] = "Settings saved";
        return View(model);
    }
}
