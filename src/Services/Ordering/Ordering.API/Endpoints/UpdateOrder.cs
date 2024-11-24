
using Ordering.Application.Dtos;
using Ordering.Application.Orders.Commands.UpdateOrder;

namespace Ordering.API.Endpoints
{

	// TODO: Accept an UpdateOrderRequest
	// TODO: Map the UpdateOrderRequest to an commaand
	// TODO: Send the command to the mediator
	// TODO: Return a success or error based on the result of the command

	public record UpdateOrderRequest(OrderDto Order);
	public record UpdateOrderResponse(bool IsSuccess);
	public class UpdateOrder : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapPut("/orders", async (UpdateOrderRequest request, ISender sender) =>
			{
				var command = request.Adapt<UpdateOrderCommand>();
				var result = await sender.Send(command);
				var response = result.Adapt<UpdateOrderResponse>();
				return Results.Ok(response);
			})
			.WithName("UpdateOrder")
			.Produces<UpdateOrderResponse>(StatusCodes.Status200OK)
			.ProducesProblem(StatusCodes.Status400BadRequest)
			.WithSummary("Updates an existing order")
			.WithDescription("Updates an existing order with the provided details");
		}
	}
}
