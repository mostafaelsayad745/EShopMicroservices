

using Ordering.Application.Dtos;

namespace Ordering.Application.Entensions
{
	public static class OrderExtensions
	{
		public static IEnumerable<OrderDto> ToOrderDtoList(this IEnumerable<Order> orders)
		{
			return orders.Select(order => new OrderDto(
									Id: order.Id.Value,
					CustomerId: order.CustomerId.Value,
					OrderName: order.OrderName.Value,
					ShippingAddress: new AddressDto(
						order.ShippingAddress.FristName,
						order.ShippingAddress.LastName,
						order.ShippingAddress.EmailAddress,
						order.ShippingAddress.AddressLine,
						order.ShippingAddress.State,
						order.ShippingAddress.Country,
						order.ShippingAddress.ZipCode),
					BillingAddress: new AddressDto(
						order.ShippingAddress.FristName,
						order.ShippingAddress.LastName,
						order.ShippingAddress.EmailAddress,
						order.ShippingAddress.AddressLine,
						order.ShippingAddress.State,
						order.ShippingAddress.Country,
						order.ShippingAddress.ZipCode),
					Payment: new PaymentDto(
						order.Payment.CardNumber,
						order.Payment.CardName ?? string.Empty,
						order.Payment.Expiration,
						order.Payment.CVV,
						order.Payment.PaymentMethod),
					Status: order.OrderStatus,
					OrderItems: order.OrderItems.Select(oi => new OrderItemDto(oi.OrderId.Value, oi.ProductId.Value, oi.Quantity, oi.Price)).ToList()


				));

		}
		public static OrderDto ToOrderDto(this Order order)
		{
			return DtoFromOrder(order);
		}

		private static OrderDto DtoFromOrder(Order order)
		{
			return new OrderDto(
				Id: order.Id.Value,
				CustomerId: order.CustomerId.Value,
				OrderName: order.OrderName.Value,
				ShippingAddress: new AddressDto(
					order.ShippingAddress.FristName,
					order.ShippingAddress.LastName,
					order.ShippingAddress.EmailAddress,
					order.ShippingAddress.AddressLine,
					order.ShippingAddress.State,
					order.ShippingAddress.Country,
					order.ShippingAddress.ZipCode),
				BillingAddress: new AddressDto(
					order.ShippingAddress.FristName,
					order.ShippingAddress.LastName,
					order.ShippingAddress.EmailAddress,
					order.ShippingAddress.AddressLine,
					order.ShippingAddress.State,
					order.ShippingAddress.Country,
					order.ShippingAddress.ZipCode),
				Payment: new PaymentDto(
					order.Payment.CardNumber,
					order.Payment.CardName ?? string.Empty,
					order.Payment.Expiration,
					order.Payment.CVV,
					order.Payment.PaymentMethod),
				Status: order.OrderStatus,
				OrderItems: order.OrderItems.Select(oi => new OrderItemDto(oi.OrderId.Value, oi.ProductId.Value, oi.Quantity, oi.Price)).ToList()
			);
		}
	}
}
