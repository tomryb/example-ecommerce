namespace Backend.ProductCatalogModule
{
    using System;
    using System.Threading.Tasks;

    public class CreateProduct
    {
        private readonly IProductRepository _productRepository;
        private readonly IAuthorizationService _authorizationService;

        public CreateProduct(IProductRepository productRepository, IAuthorizationService authorizationService)
        {
            _productRepository = productRepository;
            _authorizationService = authorizationService;
        }

        public async Task<Product> ExecuteAsync(Product product)
        {
            _authorizationService.Authorize(Permissions.CreateProduct);
            product.Id = Guid.NewGuid();
            await _productRepository.AddAsync(product);
            return product;
        }
    }
}