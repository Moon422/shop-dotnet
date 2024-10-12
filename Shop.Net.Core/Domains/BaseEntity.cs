namespace Shop.Net.Core.Domains;

public abstract class BaseEntity
{
    public int Id { get; set; }
}

public interface CreationLoggedEntity
{
    DateTime CreatedOn { get; set; }
}

public interface ModificationLoggedEntity
{
    DateTime ModifiedOn { get; set; }
}