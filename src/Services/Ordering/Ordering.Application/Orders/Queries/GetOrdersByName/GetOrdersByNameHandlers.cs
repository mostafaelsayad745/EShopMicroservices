




namespace Ordering.Application.Orders.Queries.GetOrdersByName
{
	public class GetOrdersByNameHandlers (IApplicationDbContext dbContext)
		: IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
	{
		public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
		{
			// get orders by name using dbcontext
			// return result

			var orders = await dbContext.Orders
				.Include(o => o.OrderItems)
				.AsNoTracking() // read-only operations and there is no write (update, insert, delete) operation
				.Where(o => o.OrderName.Value.Contains(query.Name))
				.OrderBy(o => o.OrderName.Value)
				.ToListAsync(cancellationToken);

			

			return new GetOrdersByNameResult(orders.ToOrderDtoList());
		}

		// we replace it with extension method because it is not a good practice to have this method in the handler

		//private List<OrderDto> ProjectToOrderDto(List<Order> orders)
		//{
		//	List<OrderDto> result = new();
		//	foreach (var order in orders)
		//	{
		//		var orderDto = new OrderDto(
		//			Id: order.Id.Value,
		//			CustomerId: order.CustomerId.Value,
		//			OrderName: order.OrderName.Value,
		//			ShippingAddress: new AddressDto(
		//				order.ShippingAddress.FristName,
		//				order.ShippingAddress.LastName,
		//				order.ShippingAddress.EmailAddress,
		//				order.ShippingAddress.AddressLine,
		//				order.ShippingAddress.State,
		//				order.ShippingAddress.Country,
		//				order.ShippingAddress.ZipCode),
		//			BillingAddress: new AddressDto(
		//				order.ShippingAddress.FristName,
		//				order.ShippingAddress.LastName,
		//				order.ShippingAddress.EmailAddress,
		//				order.ShippingAddress.AddressLine,
		//				order.ShippingAddress.State,
		//				order.ShippingAddress.Country,
		//				order.ShippingAddress.ZipCode),
		//			Payment: new PaymentDto(
		//				order.Payment.CardNumber,
		//				order.Payment.CardName ?? string.Empty,
		//				order.Payment.Expiration,
		//				order.Payment.CVV,
		//				order.Payment.PaymentMethod),
		//			Status: order.OrderStatus,
		//			OrderItems: order.OrderItems.Select(oi => new OrderItemDto( oi.OrderId.Value , oi.ProductId.Value , oi.Quantity , oi.Price)).ToList()

		//			);

		//		result.Add(orderDto);
		//	}
		//	return result;
		//}
	}
}
