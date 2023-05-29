namespace Backend.Model;

public class ProductInventoryChange
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public int Adjustment { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTimeOffset Date { get; set; }
    public Guid ChangedBy { get; set; }
}