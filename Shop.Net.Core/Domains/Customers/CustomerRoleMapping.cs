namespace Shop.Net.Core.Domains.Customers;

public class CustomerRoleMapping : BaseEntity
{
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }

    public int RoleId { get; set; }
    public Role Role { get; set; }
}