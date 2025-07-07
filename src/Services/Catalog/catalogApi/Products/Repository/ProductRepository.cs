using catalogApi.Products.IRepository;

namespace catalogApi.Products.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IConfiguration _configuration;
        public ProductRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> CreateProductAsync(Product product)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(_configuration["SqlServer:ConnectionStrings:DefaultConnection"]))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@Name", product.Name);
                    parameters.Add("@Category", product.Category);
                    parameters.Add("@Description", product.Description);
                    parameters.Add("@ImageFile", product.ImageFile);
                    parameters.Add("@Price", product.Price);

                    string sqlText = $"DECLARE @NewId INT;\r\n insert into dbo.Products(Name,Category,Description,ImageFile,Price) values (@Name,@Category,@Description,@ImageFile,@Price);SET @NewId = SCOPE_IDENTITY();\r\n\r\nSELECT @NewId AS InsertedId;";
                    string productId = await connection.ExecuteScalarAsync<string>(sqlText, parameters);
                    return productId;

                };
            }
            catch (Exception ex)
            {
                throw new ProductsServiceExceptions(ex.Message);
            }

        }
    }
}
