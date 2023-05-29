namespace Backend.ProductCatalogModule;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public Uri? MainImageUrl { get; set; }
    public Uri[] ImagesUrls { get; set; } = Array.Empty<Uri>();
}