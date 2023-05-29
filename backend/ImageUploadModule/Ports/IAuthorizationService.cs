namespace Backend.ImageUploadModule;

public interface IAuthorizationService
{
    public void Authorize(string permission);
}