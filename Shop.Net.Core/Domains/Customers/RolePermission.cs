using System.ComponentModel.DataAnnotations;

namespace Shop.Net.Core.Domains.Customers;

public class RolePermission : BaseEntity
{
    [MaxLength(64)]
    public string Permission { get; set; }

    public int RoleId { get; set; }
    public Role Role { get; set; }
}