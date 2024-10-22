using Microsoft.AspNetCore.Html;

namespace Shop.Net.Web.Admin.Models.Common;

public abstract record TableCellRenderer
{
    public abstract IHtmlContent Render(string tableIdentifier);
}
