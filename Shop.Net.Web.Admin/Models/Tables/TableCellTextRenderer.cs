using Microsoft.AspNetCore.Html;

namespace Shop.Net.Web.Admin.Models.Common;

public record TableCellTextRenderer : TableCellRenderer
{
    public override IHtmlContent Render(string tableIdentifier)
    {
        var html = $"renderText_{tableIdentifier}";
        return new HtmlString(html);
    }
}