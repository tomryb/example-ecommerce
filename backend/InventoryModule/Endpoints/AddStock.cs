namespace Backend.InventoryModule;

public class AddStock
{
    private readonly IInventoryRepository _inventoryRepository;
    private readonly IAuthorizationService _authorizationService;

    public AddStock(IInventoryRepository inventoryRepository, IAuthorizationService authorizationService)
    {
        _inventoryRepository = inventoryRepository;
        _authorizationService = authorizationService;
    }

    public async Task<int> ExecuteAsync(Guid productId, int quantity)
    {
        _authorizationService.Authorize(Permissions.AddStock);
        return await _inventoryRepository.AddStockAsync(productId, quantity);
    }
}