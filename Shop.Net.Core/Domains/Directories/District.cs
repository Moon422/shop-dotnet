using System;
using System.ComponentModel.DataAnnotations;

namespace Shop.Net.Core.Domains.Directories;

public class District : BaseEntity, ICreationLoggedEntity, IModificationLoggedEntity
{
    [MaxLength(72)]
    public string Name { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime ModifiedOn { get; set; }

    public int DivisionId { get; set; }
    public Division Division { get; set; }
}
