namespace Backend.InventoryModule;

public class RemoveStock
{
    private readonly IInventoryRepository _inventoryRepository;
    private readonly IAuthorizationService _authorizationService;

    public RemoveStock(IInventoryRepository inventoryRepository, IAuthorizationService authorizationService)
    {
        _inventoryRepository = inventoryRepository;
        _authorizationService = authorizationService;
    }

    public async Task<int> ExecuteAsync(Guid productId, int quantity)
    {
        _authorizationService.Authorize(Permissions.RemoveStock);
        return await _inventoryRepository.RemoveStockAsync(productId, quantity);
    }
}