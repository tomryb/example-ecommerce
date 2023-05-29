namespace Backend.InventoryModule;

public interface IInventoryRepository
{
    Task<int> AddStockAsync(Guid productId, int quantity);
    Task<int> RemoveStockAsync(Guid productId, int quantity);
}