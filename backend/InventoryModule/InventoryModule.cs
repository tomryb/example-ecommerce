namespace Backend.InventoryModule;

public class InventoryModule : IModule<IAuthorizationService>
{
    public ApiEndpoint[] AddModule(IAuthorizationService authorizationService)
    {
        return new[]{
            new ApiEndpoint("/products/{productId}/inventory", (Guid productId, IncrementDto increment, IInventoryRepository inventoryRepository) => increment.Quantity > 0
                ? new AddStock(inventoryRepository, authorizationService).ExecuteAsync(productId, increment.Quantity )
                : new RemoveStock(inventoryRepository, authorizationService).ExecuteAsync(productId, increment.Quantity ), HttpMethod.Put)
        };
    }
}