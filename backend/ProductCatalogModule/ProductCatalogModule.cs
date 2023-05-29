namespace Backend.ProductCatalogModule;

public class ProductCatalogModule : IModule<IAuthorizationService>
{
    public ApiEndpoint[] AddModule(IAuthorizationService authorizationService)
    {
        return new[]
        {
            new ApiEndpoint("/products", (string? name, decimal? minPrice, decimal? maxPrice, bool? onlyInStock, IProductRepository productRepository) => {
                if (name is not null || minPrice is not null || maxPrice is not null || onlyInStock is not null)
                {
                    return new ListProductsWithFilters(productRepository, authorizationService).ExecuteAsync(name, minPrice, maxPrice, onlyInStock);
                }
                else
                {
                    return new ListProducts(productRepository, authorizationService).ExecuteAsync();
                }
            }),
            new ApiEndpoint("/products/{id}", (Guid id, IProductRepository productRepository) => new GetProduct(productRepository, authorizationService).ExecuteAsync(id)),
            new ApiEndpoint("/products", (Product product, IProductRepository productRepository) => new CreateProduct(productRepository, authorizationService).ExecuteAsync(product), HttpMethod.Post),
            new ApiEndpoint("/products/{id}", (Guid id, Product product, IProductRepository productRepository) => new UpdateProduct(productRepository, authorizationService).ExecuteAsync(id, product), HttpMethod.Put),
        };
    }
}