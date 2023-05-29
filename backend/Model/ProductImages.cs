namespace Backend.Model;

public class ProductImage
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string ImageRelativePath { get; set; } = string.Empty;
    public string ImageAlt { get; set; } = string.Empty;
}