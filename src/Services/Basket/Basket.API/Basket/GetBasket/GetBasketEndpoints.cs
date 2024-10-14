

namespace Basket.API.Basket.GetBasket
{
	//public record GetBasketRequest(string UserName);

	public record GetBasketResponse(ShoppingCart ShoppingCart);
	public class GetBasketEndpoints : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapGet("/basket/{userName}", async (string userName, ISender sender) =>
			{
				var result = await sender.Send(new GetBasketQuery(userName));
				var response = result.Adapt<GetBasketResponse>();
				return Results.Ok(response);
			})
			.WithName("get product by Id")
			.Produces<GetBasketResponse>(StatusCodes.Status200OK)
			.ProducesProblem(StatusCodes.Status400BadRequest)
			.WithDescription("Get basket by user name")
			.WithSummary("Get basket by user name");

		}
	}
}
