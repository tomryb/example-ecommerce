namespace Backend.Model;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }

    public ICollection<ProductInventoryChange> InventoryChanges { get; set; } = new List<ProductInventoryChange>();
    public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();
}