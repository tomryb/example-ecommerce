namespace Backend.ImageUploadModule;

public interface IImageRepository
{
    Task<Image> AddImageAsync(Image image);
}