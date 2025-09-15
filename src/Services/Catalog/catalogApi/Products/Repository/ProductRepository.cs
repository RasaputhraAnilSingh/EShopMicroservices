
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

        public async Task<bool> DeleteProductById(Guid id)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(_configuration["SqlServer:ConnectionStrings:DefaultConnection"]))
                {
                    connection.Open();
                    string sqlText = $"Delete from dbo.Products where id = @_id";
                    var result = await connection.ExecuteAsync(sqlText, new {_id = id});
                    bool response = result > 0 ? true : false;
                    return response;

                }
                ;
            }
            catch (Exception ex)
            {
                throw new ProductsServiceExceptions(ex.Message);
            }
        }

        public async Task<List<Product>> GetAllProducts()
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(_configuration["SqlServer:ConnectionStrings:DefaultConnection"]))
                {
                    connection.Open();
                    string sqlText = $"select * dbo.Products";
                    var result = await connection.QueryAsync<Product>(sqlText);  
                    return result.ToList();
                }
                ;
            }
            catch (Exception ex)
            {
                throw new ProductsServiceExceptions(ex.Message);
            }
        }

        public async Task<Product> GetProductByCategory(string category)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(_configuration["SqlServer:ConnectionStrings:DefaultConnection"]))
                {
                    connection.Open();
                    string sqlText = $"select * from dbo.Products where category = @_category";
                    var result = await connection.QueryFirstOrDefaultAsync<Product>(sqlText, new { _category = category });
                    return result;
                }
                ;
            }
            catch (Exception ex)
            {
                throw new ProductsServiceExceptions(ex.Message);
            }
        }

        public async Task<Product> GetProductById(Guid id)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(_configuration["SqlServer:ConnectionStrings:DefaultConnection"]))
                {
                    connection.Open();
                    string sqlText = $"select * from dbo.Products where id = @_id";
                    var result = await connection.QueryFirstOrDefaultAsync<Product>(sqlText, new { _id = id });
                    return result;
                };
            }
            catch (Exception ex)
            {
                throw new ProductsServiceExceptions(ex.Message);
            }
        }
    }
}
