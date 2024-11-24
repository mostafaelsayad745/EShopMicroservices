
using Ordering.Application.Dtos;
using Ordering.Application.Orders.Queries.GetOrdersByName;

namespace Ordering.API.Endpoints
{
	// todo : Accept the order name from url
	// todo : construct a GetOrderByNameCommand
	// todo : Use the mediator to send the GetOrderByNameCommand
	// todo : retrevies and returns matching orders

	//public record GetOrderByNameRequest(string OrderName);

	public record GetOrderByNameResponse(IEnumerable<OrderDto> Orders);
	public class GetOrderByName : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapGet("orders/{OrderName}", async (string OrderName, ISender sender) =>
			{
				var result = await sender.Send(new GetOrdersByNameQuery(OrderName));
				var response = result.Adapt<GetOrderByNameResponse>();
				return Results.Ok(response);
			})
			.WithName("GetOrderByName")
			.Produces<GetOrderByNameResponse>(StatusCodes.Status200OK)
			.ProducesProblem(StatusCodes.Status400BadRequest)
			.ProducesProblem(StatusCodes.Status404NotFound)
			.WithDescription("Get orders by name")
			.WithSummary("Get orders by name");
		}
	}
}
