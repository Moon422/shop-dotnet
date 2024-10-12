using System;
using System.ComponentModel.DataAnnotations;

namespace Shop.Net.Core.Domains.Directories;

public class Address : BaseEntity, ICreationLoggedEntity, IModificationLoggedEntity
{
    [MaxLength(256)]
    public string Fullname { get; set; }

    [MaxLength(64)]
    public string FlatNumber { get; set; }

    [MaxLength(64)]
    public string HouseNumber { get; set; }

    [MaxLength(64)]
    public string RoadNumber { get; set; }

    [MaxLength(256)]
    public string Address1 { get; set; }

    [MaxLength(128)]
    public string City { get; set; }

    public int DistrictId { get; set; }
    public District District { get; set; }

    public int? StateProvinceId { get; set; }
    public Division StateProvince { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime ModifiedOn { get; set; }
}