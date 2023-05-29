namespace Backend.ProductCatalogModule;

public interface IAuthorizationService
{
    void Authorize(string permission);
}