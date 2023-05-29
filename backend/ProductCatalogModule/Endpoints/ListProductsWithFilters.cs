namespace Backend.ProductCatalogModule;

public class ListProductsWithFilters
{
    private readonly IProductRepository _productRepository;
    private readonly IAuthorizationService _authorizationService;

    public ListProductsWithFilters(IProductRepository productRepository, IAuthorizationService authorizationService)
    {
        _productRepository = productRepository;
        _authorizationService = authorizationService;
    }

    public async Task<IEnumerable<Product>> ExecuteAsync(string? name, decimal? minPrice, decimal? maxPrice, bool? onlyInStock)
    {
        _authorizationService.Authorize(Permissions.ListProductsWithFilters);
        return await _productRepository.ListAsync(name, minPrice, maxPrice, onlyInStock);
    }
}