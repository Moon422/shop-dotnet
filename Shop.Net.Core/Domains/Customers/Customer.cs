using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Shop.Net.Core.Domains.Directories;

namespace Shop.Net.Core.Domains.Customers;

public class Customer : BaseEntity, ICreationLoggedEntity, IModificationLoggedEntity, ISoftDeletedEntity
{
    [MaxLength(160)]
    public string FirstName { get; set; }

    [MaxLength(72)]
    public string LastName { get; set; }

    public Gender Gender { get; set; }

    [MaxLength(14)]
    public string PhoneNumber { get; set; }

    [MaxLength(128)]
    [EmailAddress]
    public string Email { get; set; }

    public int? DefaultBillingAddressId { get; set; }
    public Address DefaultBillingAddress { get; set; }

    public int? DefaultShippingAddressId { get; set; }
    public Address DefaultShippingAddress { get; set; }

    [MaxLength(128)]
    public string Theme { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime ModifiedOn { get; set; }

    public DateTime DeletedOn { get; set; }

    public ICollection<Role> Roles { get; set; }

    public ICollection<CustomerPermission> CustomerPermissions { get; set; }

    public Password Password { get; set; }
}
