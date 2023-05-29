using Backend.Model;
using Microsoft.EntityFrameworkCore;

namespace Backend;

public class BackendDbContext : DbContext
{
    public BackendDbContext(DbContextOptions<BackendDbContext> options)
        : base(options) { }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<ProductInventoryChange> ProductInventoryChanges => Set<ProductInventoryChange>();
    public DbSet<ProductImage> ProductImages => Set<ProductImage>();
}