
using catalogApi.Products.Repository;

namespace catalogApi.Products.ProductById
{
    public record ProductByIdQuery(Guid id) : IQuery<ProductByIdResult>;
    public record ProductByIdResult(Product product);
    public class ProductByIdHandler : IQueryHandler<ProductByIdQuery, ProductByIdResult>
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductByIdHandler> _logger;
        public ProductByIdHandler(IProductRepository productRepository,ILogger<ProductByIdHandler> logger) 
        { 
            _productRepository = productRepository;
            _logger = logger;
        
        }
        public async Task<ProductByIdResult> Handle(ProductByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _productRepository.GetProductById(request.id);
            return new ProductByIdResult(result);

        }
    }
}
