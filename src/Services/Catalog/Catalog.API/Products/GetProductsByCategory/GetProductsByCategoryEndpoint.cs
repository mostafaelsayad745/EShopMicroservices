
namespace Catalog.API.Products.GetProductsByCategory
{
	public record GetProductsByCategoryResponse(IEnumerable<Product> Products);
	public class GetProductsByCategoryEndpoint : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapGet("/products/category/{category}", async (string category , ISender sender) =>
			{
				var result = await sender.Send(new GetProductsByCategoryRequest(category));
				var response = result.Adapt<GetProductsByCategoryResponse>();
				return Results.Ok(response);
			})
			.WithName("GetProductsByCategory")
			.Produces<GetProductsByCategoryResponse>(StatusCodes.Status200OK)
			.ProducesProblem(StatusCodes.Status404NotFound)
			.WithDescription("Get products by category")
			.WithSummary("Get products by category");
		}
	}
}
