namespace Shop.Net.Core.Domains.Inventory;

public class ProductCategoryMapping : BaseEntity
{
    public int ProductId { get; set; }
    public Product Product { get; set; }

    public int CategoryId { get; set; }
    public Category Category { get; set; }
}