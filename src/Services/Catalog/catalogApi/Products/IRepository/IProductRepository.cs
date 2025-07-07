namespace catalogApi.Products.IRepository
{
    public interface IProductRepository
    {
        Task<Guid> CreateProductAsync(Product products);
    }
}
