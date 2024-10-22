using System.Collections.Generic;

namespace Shop.Net.Web.Admin.Models.Common;

public record PagedListModel<T> where T : BaseModel
{
    public IList<T> Data { get; set; }

    public int PageIndex { get; set; }

    public int TotalItemCount { get; set; }

    public int MaximumPageItemCount { get; set; }

    public int CurrentPageItemCount { get; set; }

    public int NumberOfPages { get; set; }

    public bool HasPrevious { get; set; }

    public bool HasNext { get; set; }
}