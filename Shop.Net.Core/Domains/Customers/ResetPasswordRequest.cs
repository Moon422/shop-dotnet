using System;
using System.ComponentModel.DataAnnotations;

namespace Shop.Net.Core.Domains.Customers;

public class ResetPasswordRequest : BaseEntity
{
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }

    [MaxLength(6)]
    public string OtpCode { get; set; }

    public bool IsActive { get; set; }

    public DateTime ValidTill { get; set; }
}