using Backend.Model;

namespace Backend.Adapters;

public class AuthorizationAdapters : ProductCatalogModule.IAuthorizationService, InventoryModule.IAuthorizationService
{
    private readonly Action<Permission> _authorize;

    public AuthorizationAdapters(Action<Permission> authorize)
    {
        _authorize = authorize;
    }

    public void Authorize(string permission)
    {
        switch (permission)
        {
            case ProductCatalogModule.Permissions.GetProduct:
                _authorize(Permission.GetProduct);
                break;

            case ProductCatalogModule.Permissions.ListProducts:
                _authorize(Permission.ListProducts);
                break;

            case ProductCatalogModule.Permissions.ListProductsWithFilters:
                _authorize(Permission.ListProductsWithFilters);
                break;

            case ProductCatalogModule.Permissions.CreateProduct:
                _authorize(Permission.CreateProduct);
                break;

            case ProductCatalogModule.Permissions.UpdateProduct:
                _authorize(Permission.UpdateProduct);
                break;

            case InventoryModule.Permissions.AddStock:
                _authorize(Permission.AddStock);
                break;

            case InventoryModule.Permissions.RemoveStock:
                _authorize(Permission.RemoveStock);
                break;

            default:
                throw new NotImplementedException();
        };
    }
}