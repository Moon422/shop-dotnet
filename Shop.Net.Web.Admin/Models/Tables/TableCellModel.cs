namespace Shop.Net.Web.Admin.Models.Common;

public record TableCellModel : BaseModel
{
    public string PropertyName { get; set; }
    public string Title { get; set; }
    public string ClassNames { get; set; }
    public TableCellRenderer Renderer { get; set; }
}
