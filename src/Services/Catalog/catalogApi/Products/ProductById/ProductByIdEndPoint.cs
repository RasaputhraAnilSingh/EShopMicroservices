
namespace catalogApi.Products.ProductById
{
    public record ProductByIdResponse(Product product);
    public class ProductByIdEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new ProductByIdQuery(id));
                var response = result.Adapt<ProductByIdResponse>();
                return Results.Ok(response);
            });
        }
    }
}
