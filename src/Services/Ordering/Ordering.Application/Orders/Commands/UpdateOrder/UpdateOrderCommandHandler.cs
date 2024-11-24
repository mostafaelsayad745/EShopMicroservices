


using Ordering.Application.Dtos;

namespace Ordering.Application.Orders.Commands.UpdateOrder
{
	public class UpdateOrderCommandHandler (IApplicationDbContext dbContext) 
		: ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
	{
		
		public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
		{
			// TODO: update order entity from command object
			// TODO: save the order to the database
			// TODO : return result

			var orderId = OrderId.Of(command.Order.Id);

			var order = await dbContext.Orders.FindAsync([orderId],cancellationToken : cancellationToken);

			if (order is null)
			{
				throw new OrderNotFoundException(command.Order.Id);
			}

			UpdateOrderWithNewValues(order, command.Order);

			await dbContext.SaveChangesAsync(cancellationToken);

			return new UpdateOrderResult(true);
		}

		private void UpdateOrderWithNewValues(Order order, OrderDto orderDto)
		{
			var orderShippingAddress = Address.Of(
				orderDto.ShippingAddress.FirstName,
				orderDto.ShippingAddress.LastName,
				orderDto.ShippingAddress.EmailAddress,
				orderDto.ShippingAddress.AddressLine,
				orderDto.ShippingAddress.State,
				orderDto.ShippingAddress.Country,
				orderDto.ShippingAddress.ZipCode
			);

			var orderBillingAddress = Address.Of(
				orderDto.BillingAddress.FirstName,
				orderDto.BillingAddress.LastName,
				orderDto.BillingAddress.EmailAddress,
				orderDto.BillingAddress.AddressLine,
				orderDto.BillingAddress.State,
				orderDto.BillingAddress.Country,
				orderDto.BillingAddress.ZipCode
			);

			var payment = Payment.Of(
				orderDto.Payment.CardNumber,
				orderDto.Payment.CardName,
				orderDto.Payment.Expiration,
				orderDto.Payment.Cvv,
				orderDto.Payment.PaymentMethod
			);

			order.Update(
				orderName: OrderName.Of(orderDto.OrderName),
				shippingAddress: orderShippingAddress,
				billingAddress: orderBillingAddress,
				payment: payment,
				status: orderDto.Status
			);

			// Update other order properties here
		}
	}
	
}
