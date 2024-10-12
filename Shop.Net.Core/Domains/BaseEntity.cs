using System;

namespace Shop.Net.Core.Domains;

public abstract class BaseEntity
{
    public int Id { get; set; }
}

public interface ICreationLoggedEntity
{
    DateTime CreatedOn { get; set; }
}

public interface IModificationLoggedEntity
{
    DateTime ModifiedOn { get; set; }
}

public interface ISoftDeletedEntity
{
    public bool IsDeleted { get; set; }
    DateTime DeletedOn { get; set; }
}