namespace Shop.Net.Core.Domains.Customers;

public class RolePermission : BaseEntity
{
    public string Permission { get; set; }

    public int RoleId { get; set; }
    public Role Role { get; set; }
}