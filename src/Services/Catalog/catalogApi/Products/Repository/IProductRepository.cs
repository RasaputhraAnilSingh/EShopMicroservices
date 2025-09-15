namespace catalogApi.Products.Repository
{
    public interface IProductRepository
    {
        Task<string> CreateProductAsync(Product products);
        Task<Product> GetProductById(Guid id);
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProductByCategory(string category);
        Task<bool> DeleteProductById(Guid id);
    }
}
