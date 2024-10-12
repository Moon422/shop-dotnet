using System.ComponentModel.DataAnnotations;

namespace Shop.Net.Core.Domains.Customers;

public class CustomerPermission : BaseEntity
{
    [MaxLength(64)]
    public string Permission { get; set; }

    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
}
