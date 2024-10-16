using System;

namespace Shop.Net.Web.Admin.Models.Customers;

public record CustomerModel : BaseEntityModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int GenderId { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }
    public DateTime DeletedOn { get; set; }
}
