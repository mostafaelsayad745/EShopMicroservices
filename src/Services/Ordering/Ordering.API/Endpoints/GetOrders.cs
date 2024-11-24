using BuildingBlocks.Pagination;
using Ordering.Application.Dtos;
using Ordering.Application.Orders.Queries.GetOrders;

namespace Ordering.API.Endpoints
{
	// todo : Accept pagination parameters from url
	// todo : construct a GetOrdersQuery by using the pagination parameters
	// todo : Use the mediator to send the GetOrdersQuery
	// todo : retrevies and returns orders in paginated form

	//public record GetOrdersRequest(PaginationRequest PaginationRequest);
	public record GetOrdersResponse(PaginatedResult<OrderDto> Orders);
	public class GetOrders : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapGet("orders", async([AsParameters] PaginationRequest paginationRequest, ISender sender) =>
			{
				var result = await sender.Send(new GetOrdersQuery(paginationRequest));
				var response = result.Adapt<GetOrdersResponse>();
				return Results.Ok(response);
			})
			.WithName("GetOrders")
			.Produces<GetOrdersResponse>(StatusCodes.Status200OK)
			.ProducesProblem(StatusCodes.Status400BadRequest)
			.ProducesProblem(StatusCodes.Status404NotFound)
			.WithDescription("Get orders")
			.WithSummary("Get orders");
		}
	}
}
