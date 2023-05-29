using Backend.ImageUploadModule;

namespace Backend.Implementations;

public class ImageRepository : IImageRepository
{
    private readonly BackendDbContext _dbContext;

    public ImageRepository(BackendDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Image> AddImageAsync(Image image)
    {

        await _dbContext.ProductImages.AddAsync(new Model.ProductImage
        {
            Id = image.Id,
            ProductId = image.ProductId,
            ImageAlt = "TODO",
            ImageRelativePath = image.Url
        });
        await _dbContext.SaveChangesAsync();

        return image;
    }
}