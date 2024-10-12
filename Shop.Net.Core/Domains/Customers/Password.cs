using System.ComponentModel.DataAnnotations;

namespace Shop.Net.Core.Domains.Customers;

public class Password : BaseEntity
{
    [MaxLength(60)]
    public string PasswordHash { get; set; }

    public int CustomerId { get; set; }
    public Customer customer { get; set; }
}