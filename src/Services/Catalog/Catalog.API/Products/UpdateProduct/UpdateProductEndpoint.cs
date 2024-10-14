
namespace Catalog.API.Products.UpdateProduct
{
	public record UpdateProductRequest(Guid Id, string Name, List<string> Category, string Description, decimal Price, string ImageFile);

	public record UpdateProductResponse(bool IsSuccess);

	public class UpdateProductEndpoint : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapPut("/products", async (UpdateProductRequest request, ISender sender) =>
			{
				var command = request.Adapt<UpdateProductCommand>();
				var result = await sender.Send(command);
				var response = result.Adapt<UpdateProductResponse>();
				return Results.Ok(response);
			})
			.WithName("UpdateProduct")
			.Produces<UpdateProductResponse>(StatusCodes.Status200OK)
			.Produces(StatusCodes.Status404NotFound)
			.Produces(StatusCodes.Status400BadRequest)
			.WithDescription("Update a product")
			.WithSummary("Update a product");
		}
	}
}
