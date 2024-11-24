
using Ordering.Application.Dtos;
using Ordering.Application.Orders.Commands.CreateOrder;

namespace Ordering.API.Endpoints
{
	// TODO: Accept a CreateOrderRequest object
	// TODO: Maps the request to CreateOrderCommand
	// TODO: uss the mediator to send the command to corsponding handler
	// TODO: Return a CreateOrderResponse with the created order id

	public record CreateOrderRequest(OrderDto Order);
	public record CreateOrderResponse(Guid Id);

	public class CreateOrder : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapPost("/orders", async (CreateOrderRequest request, ISender sender) =>
			{
				var command = request.Adapt<CreateOrderCommand>();

				var result = await sender.Send(command);

				var response = result.Adapt<CreateOrderResponse>();

				return Results.Created($"/orders/{response.Id}", response);
			})
			.WithName("CreateOrder")
			.Produces<CreateOrderResponse>(StatusCodes.Status201Created)
			.ProducesProblem(StatusCodes.Status400BadRequest)
			.WithSummary("Create Order")
			.WithDescription("Create Order");
		}
	}
}
