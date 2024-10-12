using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shop.Net.Core.Domains.Customers;

public class Role : BaseEntity
{
    [MaxLength(72)]
    public string Name { get; set; }

    public ICollection<Customer> Customers { get; set; }
}
