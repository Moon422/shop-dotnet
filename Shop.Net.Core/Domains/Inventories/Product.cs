﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shop.Net.Core.Domains.Inventories;

public class Product : BaseEntity, ICreationLoggedEntity, IModificationLoggedEntity, ISoftDeletedEntity
{
    [MaxLength(128)]
    public string Name { get; set; }

    [MaxLength(280)]
    public string ShortDescription { get; set; }

    [MaxLength(1024)]
    public string LongDescription { get; set; }

    public decimal BasePrice { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime ModifiedOn { get; set; }

    public DateTime DeletedOn { get; set; }

    public ICollection<Category> Categories { get; set; }
}
