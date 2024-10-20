using System.Collections.Generic;

namespace Shop.Net.Web.Admin.Models.Common;

public record TableModel : BaseModel
{
    public IList<string> Columns { get; set; }
}