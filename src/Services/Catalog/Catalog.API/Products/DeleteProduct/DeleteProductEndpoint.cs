namespace Catalog.API.Products.DeleteProduct
{
	public record DeleteProductResponce(bool IsSuccess);
	public class DeleteProductEndpoint : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapDelete("/products/{id}", async (ISender sender, Guid id) =>
			{
				var result = await sender.Send(new DeleteProductCommand(id));
				var response = result.Adapt<DeleteProductResponce>();
				return Results.Ok(response);
			})
			.WithName("DeleteProduct")
			.Produces<DeleteProductResponce>(StatusCodes.Status200OK)
			.ProducesProblem(StatusCodes.Status400BadRequest)
			.ProducesProblem(StatusCodes.Status404NotFound)
			.WithDescription("Delete a product")
			.WithSummary("Delete a product");
		}
	}
}
