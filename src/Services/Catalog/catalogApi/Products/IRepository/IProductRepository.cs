namespace catalogApi.Products.IRepository
{
    public interface IProductRepository
    {
        Task<string> CreateProductAsync(Product products);
    }
}
