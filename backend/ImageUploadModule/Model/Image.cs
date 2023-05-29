namespace Backend.ImageUploadModule;

public class Image
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string Url { get; set; } = string.Empty;
}