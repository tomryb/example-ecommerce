namespace Backend.InventoryModule;

public interface IAuthorizationService
{
    public void Authorize(string permission);
}