namespace Backend.ImageUploadModule;

public interface IImageStorageService
{
    Task<string> UploadImageAsync(Guid imageId, Stream imageStream);
}