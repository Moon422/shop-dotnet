namespace Shop.Net.Web.Admin.Models;

public abstract record BaseEntityModel : BaseModel
{
    public int Id { get; set; }
}
