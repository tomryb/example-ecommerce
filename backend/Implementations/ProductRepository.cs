using Backend.ProductCatalogModule;
using Microsoft.EntityFrameworkCore;

namespace Backend.Implementations;

public class ProductRepository : IProductRepository
{
    private readonly BackendDbContext _dbContext;
    private readonly IConfiguration _configuration;

    public ProductRepository(BackendDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _configuration = configuration;
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
        Model.Product? product = await _dbContext.Products.AsQueryable().Include(x => x.InventoryChanges).Include(x => x.Images).SingleOrDefaultAsync(x => x.Id == id);
        return product == null ? new Product { Id = Guid.Empty } : new Product
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Quantity = product.InventoryChanges.Sum(x => x.Adjustment),
            MainImageUrl = product.Images.Any() ? new Uri($"{_configuration.GetValue<string>("Images:RootUrl")}/${product.Images.First().ImageRelativePath}") : null,
            ImagesUrls = product.Images.Select(x => new Uri($"{_configuration.GetValue<string>("Images:RootUrl")}/${x.ImageRelativePath}")).ToArray()
        };
    }

    public async Task<IEnumerable<Product>> ListAsync()
    {
        return await _dbContext.Products.AsQueryable().Include(x => x.InventoryChanges).Include(x => x.Images).Select(x => new Product
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            Price = x.Price,
            Quantity = x.InventoryChanges.Sum(x => x.Adjustment),
            MainImageUrl = x.Images.Any() ? new Uri($"{_configuration.GetValue<string>("Images:RootUrl")}/${x.Images.First().ImageRelativePath}") : null,
            ImagesUrls = x.Images.Select(x => new Uri($"{_configuration.GetValue<string>("Images:RootUrl")}/${x.ImageRelativePath}")).ToArray()
        }).ToListAsync();
    }

    public async Task<IEnumerable<Product>> ListAsync(string? name, decimal? minPrice, decimal? maxPrice, bool? onlyInStock)
    {
        return await _dbContext.Products
            .AsQueryable()
            .Include(x => x.InventoryChanges)
            .Include(x => x.Images)
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
                Quantity = x.InventoryChanges.Sum(x => x.Adjustment),
                MainImageUrl = x.Images.Any() ? new Uri($"{_configuration.GetValue<string>("Images:RootUrl")}/${x.Images.First().ImageRelativePath}") : null,
                ImagesUrls = x.Images.Select(x => new Uri($"{_configuration.GetValue<string>("Images:RootUrl")}/${x.ImageRelativePath}")).ToArray()
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