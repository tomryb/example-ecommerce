using Backend.ImageUploadModule;

namespace Backend.MockImplementations;

public class MockImageUploadService : IImageStorageService
{
    public Task<string> UploadImageAsync(Guid imageId, Stream imageStream)
    {
        imageStream.Dispose();
        return Task.FromResult("https://i.pinimg.com/originals/4e/02/f7/4e02f7d306773c0b6a0a4e58ceea2174.jpg");
    }
}