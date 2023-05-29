using System.Collections.Generic;
namespace Backend.ProductCatalogModule;

public interface IProductRepository
{
    Task<Product> GetAsync(Guid id);
    Task<IEnumerable<Product>> ListAsync();
    Task<IEnumerable<Product>> ListAsync(string? name, decimal? minPrice, decimal? maxPrice, bool? onlyInStock);
    Task<Product> AddAsync(Product product);
    Task<Product> UpdateAsync(Product product);
}