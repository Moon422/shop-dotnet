using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shop.Net.Core.Domains.Directories;

public class Country : BaseEntity, CreationLoggedEntity, ModificationLoggedEntity
{
    [MaxLength(72)]
    public string Name { get; set; }

    public string ThreeLetterCode { get; set; }

    public string TwoLetterCode { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime ModifiedOn { get; set; }

    public ICollection<StateProvince> StateProvinces { get; set; }
}