using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shop.Net.Core.Domains.Directories;

public class Division : BaseEntity, ICreationLoggedEntity, IModificationLoggedEntity
{
    [MaxLength(72)]
    public string Name { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime ModifiedOn { get; set; }

    public int CountryId { get; set; }
    public Country Country { get; set; }

    public ICollection<District> Districts { get; set; }
}
