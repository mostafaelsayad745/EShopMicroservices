

namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer
{
	public class GetOrdersByCustomerHandler (IApplicationDbContext dbContext) 
		: IRequestHandler<GetOrdersByCustomerQuery, GetOrdersByCustomerResult>
	{
		public async Task<GetOrdersByCustomerResult> Handle(GetOrdersByCustomerQuery query, CancellationToken cancellationToken)
		{
			// TODO: Get orders by customer id using dbContext
			// return result

			var orders = await dbContext.Orders
				.Include(o => o.OrderItems)
				.AsNoTracking()
				.Where(o => o.CustomerId == CustomerId.Of(query.CustomerId))
				.OrderBy(o => o.OrderName.Value)
				.ToListAsync(cancellationToken);

			return new GetOrdersByCustomerResult(orders.ToOrderDtoList());
		}
	}
}
