using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shop.Net.Core.Domains.Directories;

public class Country : BaseEntity, CreationLoggedEntity, ModificationLoggedEntity
{
    [MaxLength(72)]
    public string Name { get; set; }

    [MaxLength(3)]
    public string ThreeLetterCode { get; set; }

    [MaxLength(2)]
    public string TwoLetterCode { get; set; }

    [MaxLength(3)]
    public string PhoneCode { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime ModifiedOn { get; set; }

    public ICollection<StateProvince> StateProvinces { get; set; }
}