using Ordering.Application.Dtos;
using Ordering.Application.Orders.Queries.GetOrdersByCustomer;

namespace Ordering.API.Endpoints
{
	// TODO: Accept the customer id from url
	// TODO: construct a GetOrdersByCustomerCommand
	// TODO: Use the mediator to send the GetOrdersByCustomerCommand
	// TODO: retrevies and returns matching orders

	//public record GetOrdersByCustomerRequest(Guid CustomerId);

	public record GetOrdersByCustomerResponse(IEnumerable<OrderDto> Orders);
	public class GetOrdersByCustomer : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapGet("orders/customer/{CustomerId}", async (Guid CustomerId, ISender sender) =>
			{
				var result = await sender.Send(new GetOrdersByCustomerQuery(CustomerId));
				var response = result.Adapt<GetOrdersByCustomerResponse>();
				return Results.Ok(response);
			})
			.WithName("GetOrdersByCustomer")
			.Produces<GetOrdersByCustomerResponse>(StatusCodes.Status200OK)
			.ProducesProblem(StatusCodes.Status400BadRequest)
			.ProducesProblem(StatusCodes.Status404NotFound)
			.WithDescription("Get orders by customer")
			.WithSummary("Get orders by customer");
		}
	}
	
}
