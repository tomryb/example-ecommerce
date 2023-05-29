using Backend.InventoryModule;
using Backend.Model;

namespace Backend.Implementations;

public class InventoryRepository : IInventoryRepository
{
    private readonly BackendDbContext _dbContext;

    public InventoryRepository(BackendDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> AddStockAsync(Guid productId, int quantity)
    {
        await _dbContext.ProductInventoryChanges.AddAsync(new ProductInventoryChange
        {
            ProductId = productId,
            Adjustment = quantity,
            ChangedBy = Guid.Empty, // TODO
            Date = DateTimeOffset.UtcNow,
            Description = "Manual adjustment",
            Id = Guid.NewGuid()
        });

        await _dbContext.SaveChangesAsync();

        return _dbContext.ProductInventoryChanges.Where(x => x.ProductId == productId).Sum(x => x.Adjustment);
    }

    public async Task<int> RemoveStockAsync(Guid productId, int quantity)
    {
        int currentQuantity = _dbContext.ProductInventoryChanges.Where(x => x.ProductId == productId).Sum(x => x.Adjustment);

        if (currentQuantity < (-quantity))
        {
            throw new InvalidOperationException("Not enough stock");
        }

        await _dbContext.ProductInventoryChanges.AddAsync(new ProductInventoryChange
        {
            ProductId = productId,
            Adjustment = quantity,
            ChangedBy = Guid.Empty, // TODO
            Date = DateTimeOffset.UtcNow,
            Description = "Manual adjustment",
            Id = Guid.NewGuid()
        });
        await _dbContext.SaveChangesAsync();

        return currentQuantity - (-quantity);
    }
}