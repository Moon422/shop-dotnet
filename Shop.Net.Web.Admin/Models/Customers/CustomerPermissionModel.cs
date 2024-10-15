namespace Shop.Net.Web.Admin.Models;

public record CustomerPermissionModel : BaseEntityModel
{
    public string Permission { get; set; }
}