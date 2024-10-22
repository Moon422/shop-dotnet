using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Shop.Net.Web.Admin.Models.Common;

public record TableModel : BaseModel
{
    public string Identifier { get; set; }
    public IList<TableCellModel> Columns { get; set; }
    public string ReadUrl { get; set; }
    public IList<SelectListItem> AvailablePageSizes { get; set; }
}
