

using BuildingBlocks.Pagination;
using Ordering.Application.Dtos;

namespace Ordering.Application.Orders.Queries.GetOrders
{
	public class GetOrdersHandler(IApplicationDbContext dbContext)
		: IRequestHandler<GetOrdersQuery, GetOrdersResult>
	{
		public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
		{
			var pageIndex = query.PaginationRequest.PageIndex;
			var pageSize = query.PaginationRequest.PageSize;

			var totalOrders = await dbContext.Orders.LongCountAsync(cancellationToken);

			var orders = await dbContext.Orders
				.Include(o => o.OrderItems)
				.AsNoTracking()
				.OrderBy(o => o.OrderName.Value)
				.Skip(pageIndex * pageSize)
				.Take(pageSize)
				.ToListAsync(cancellationToken);

			return new GetOrdersResult(
				new PaginatedResult<OrderDto>
				(
					pageIndex,
					pageSize,
					totalOrders,
					orders.ToOrderDtoList()
				));
		}
	}
}
