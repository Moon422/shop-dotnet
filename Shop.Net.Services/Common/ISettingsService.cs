using System.Threading.Tasks;
using Shop.Net.Core.Settings;

namespace Shop.Net.Services.Common;

public interface ISettingsService
{
    Task SaveSettingsAsync(ISettings settings);
}
