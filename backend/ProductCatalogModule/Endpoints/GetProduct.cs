namespace Backend.ProductCatalogModule;

public class GetProduct
{
    private readonly IProductRepository _productRepository;
    private readonly IAuthorizationService _authorizationService;

    public GetProduct(IProductRepository productRepository, IAuthorizationService authorizationService)
    {
        _productRepository = productRepository;
        _authorizationService = authorizationService;
    }

    public async Task<Product> ExecuteAsync(Guid id)
    {
        _authorizationService.Authorize(Permissions.GetProduct);
        return await _productRepository.GetAsync(id);
    }
}