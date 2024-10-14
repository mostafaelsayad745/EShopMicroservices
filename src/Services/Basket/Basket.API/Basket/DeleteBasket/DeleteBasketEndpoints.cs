
namespace Basket.API.Basket.DeleteBasket
{
	//public record DeleteBasketRequest(string UserName);

	public record DeleteBasketResponse(bool IsSuccess);
	public class DeleteBasketEndpoints : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapDelete("/basket/{userName}", async (string UserName, ISender sender) =>
			{
				var result = await sender.Send(new DeleteBasketCommand(UserName));
				var response = result.Adapt<DeleteBasketResponse>();
				return Results.Ok(response);
			})
			.WithName("Delete Product")
			.Produces<DeleteBasketResponse>(StatusCodes.Status200OK)
			.ProducesProblem(StatusCodes.Status400BadRequest)
			.ProducesProblem(StatusCodes.Status404NotFound)
			.WithDescription("Delete a Product")
			.WithSummary("Delete a Product");
		}
	}
	
}
