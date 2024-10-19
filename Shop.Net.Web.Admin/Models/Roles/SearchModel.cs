using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Shop.Net.Web.Admin.Models;

public record SearchModel : BaseModel
{
    public SearchModel()
    {
        AvailabePageSizes = [
            new SelectListItem("5", "5"),
            new SelectListItem("10", "10"),
            new SelectListItem("15", "15")
        ];
    }

    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public IList<SelectListItem> AvailabePageSizes { get; set; }
}
