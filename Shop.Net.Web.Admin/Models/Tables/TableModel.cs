using System.Collections.Generic;

namespace Shop.Net.Web.Admin.Models.Common;

public record TableModel : BaseModel
{
    public string Identifier { get; set; }
    public IList<TableCellModel> Columns { get; set; }
}
