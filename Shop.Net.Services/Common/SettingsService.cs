using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Shop.Net.Core.Settings;
using Shop.Net.Core.Settings.Attributes;

namespace Shop.Net.Services.Common;

public class SettingsService : ISettingsService
{
    public async Task SaveSettingsAsync(ISettings settings)
    {
        ArgumentNullException.ThrowIfNull(settings);

        if (settings.GetType().GetCustomAttribute<SettingsAttribute>() is SettingsAttribute sa)
        {
            var directory = sa.Directory;
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var filePath = sa.FullPath;
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }

            var settingsContent = JsonConvert.SerializeObject(settings);
            if (string.IsNullOrWhiteSpace(settingsContent))
            {
                settingsContent = "{}";
            }

            await File.WriteAllTextAsync(filePath, settingsContent, Encoding.UTF8);
        }
    }
}