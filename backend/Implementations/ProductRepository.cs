using Backend.ProductCatalogModule;
using Microsoft.EntityFrameworkCore;

namespace Backend.Implementations;

public class ProductRepository : IProductRepository
{
    private readonly BackendDbContext _dbContext;

    public ProductRepository(BackendDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Product> AddAsync(Product product)
    {
        await _dbContext.Products.AddAsync(new Model.Product
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price
        });
        await _dbContext.SaveChangesAsync();
        return product;
    }

    public async Task<Product> GetAsync(Guid id)
    {
        Model.Product? product = await _dbContext.Products.FindAsync(id);
        return product == null ? new Product { Id = Guid.Empty } : new Product
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Quantity = product.InventoryChanges.Sum(x => x.Adjustment)
        };
    }

    public async Task<IEnumerable<Product>> ListAsync()
    {
        return await _dbContext.Products.Select(x => new Product
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            Price = x.Price,
            Quantity = x.InventoryChanges.Sum(x => x.Adjustment)
        }).ToListAsync();
    }

    public async Task<IEnumerable<Product>> ListAsync(string? name, decimal? minPrice, decimal? maxPrice, bool? onlyInStock)
    {
        return await _dbContext.Products
            .Where(x => name == null || x.Name.Contains(name))
            .Where(x => minPrice == null || x.Price >= minPrice)
            .Where(x => maxPrice == null || x.Price <= maxPrice)
            .Where(x => onlyInStock == null || x.InventoryChanges.Sum(x => x.Adjustment) > 0)
            .Select(x => new Product
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                Quantity = x.InventoryChanges.Sum(x => x.Adjustment)
            }).ToListAsync();
    }

    public async Task<Product> UpdateAsync(Product product)
    {
        _dbContext.Products.Update(new Model.Product
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price
        });
        await _dbContext.SaveChangesAsync();
        return product;
    }
}