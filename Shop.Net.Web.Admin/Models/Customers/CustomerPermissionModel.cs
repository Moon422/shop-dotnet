namespace Shop.Net.Web.Admin.Models.Customers;

public record CustomerPermissionModel : BaseEntityModel
{
    public string Permission { get; set; }
}