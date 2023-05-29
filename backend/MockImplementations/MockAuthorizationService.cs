using Backend.Model;

namespace Backend.MockImplementations;

public class MockAuthorizationService
{
    public void Authorize(Permission permission)
    {
        // Do nothing
        // In regular implementation, this would throw an exception if the user is not authorized
        switch (permission)
        {
            case Permission.GetProduct:
                break;
            case Permission.ListProducts:
                break;
            case Permission.ListProductsWithFilters:
                break;
            case Permission.CreateProduct:
                break;
            case Permission.UpdateProduct:
                break;
            case Permission.AddStock:
                break;
            case Permission.RemoveStock:
                break;
            default:
                throw new NotImplementedException();
        }
    }
}