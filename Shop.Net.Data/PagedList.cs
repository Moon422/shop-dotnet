using System.Collections.Generic;
using Shop.Net.Core.Domains;

namespace Shop.Net.Data;

public class PagedList<T> where T : BaseEntity
{
    public IList<T> Data { get; set; }

    public int PageIndex { get; set; }

    public int TotalItemCount { get; set; }

    public int MaximumPageItemCount { get; set; }

    public int CurrentPageItemCount => Data.Count;

    public int NumberOfPages
    {
        get
        {
            int quotient = TotalItemCount / MaximumPageItemCount;
            int reminder = TotalItemCount % MaximumPageItemCount;

            return quotient + (reminder > 0 ? 1 : 0);
        }
    }

    public bool HasPrevious => PageIndex > 0;

    public bool HasNext => PageIndex * MaximumPageItemCount + CurrentPageItemCount < TotalItemCount;
}