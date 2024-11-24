
using Ordering.Application.Orders.Commands.DeleteOrder;

namespace Ordering.API.Endpoints
{
	// TODO: Accept the order id from url
	// TODO: construct a DeleteOrderCommand
	// TODO: Use the mediator to send the DeleteOrderCommand
	// TODO: return success or failure response

	//public record DeleteOrderRequest(Guid OrderId);
	public record DeleteOrderResponse(bool Success);
	public class DeleteOrder : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapDelete("/orders/{id}", async (Guid Id, ISender sender) =>
			{
				var result = await sender.Send(new DeleteOrderCommand(Id));
				var response = result.Adapt<DeleteOrderResponse>();
				return Results.Ok(response);
			})
			.WithName("DeleteOrder")
			.Produces<DeleteOrderResponse>(StatusCodes.Status200OK)
			.ProducesProblem(StatusCodes.Status400BadRequest)
			.ProducesProblem(StatusCodes.Status404NotFound)
			.WithDescription("Deletes an order by id")
			.WithSummary("Deletes an order by id");
		}
	}
}
