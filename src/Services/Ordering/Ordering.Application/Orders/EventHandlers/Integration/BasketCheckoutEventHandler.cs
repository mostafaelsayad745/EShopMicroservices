
using BuildingBlocks.Messaging.Events;
using MassTransit;
using Ordering.Application.Dtos;
using Ordering.Application.Orders.Commands.CreateOrder;

namespace Ordering.Application.Orders.EventHandlers.Integration
{
	public class BasketCheckoutEventHandler (ISender sender , ILogger<BasketCheckoutEventHandler> logger)
		: IConsumer<BasketCheckoutEvent>
	{
		public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
		{
			// todo : create new order and start new order fulfillment process
			logger.LogInformation("Integration Event handled: {IntegrationEvent}",  context.Message.GetType().Name);

			var command = MapToCreateOrderCommand(context.Message);
			await sender.Send(command);

		}

		private CreateOrderCommand MapToCreateOrderCommand(BasketCheckoutEvent message)
		{
			// Create full order with incoming event data
			var addressDto = new AddressDto(message.FirstName, message.LastName, message.EmailAddress, message.AddressLine, message.Country, message.State, message.ZipCode);
			var paymentDto = new PaymentDto(message.CardName, message.CardNumber, message.Expiration, message.CVV, message.PaymentMethod);
			var orderId = Guid.NewGuid();
			// Map the order items from the message
			var orderItems = message.OrderItems.Select(item => new OrderItemDto(
				orderId,
				item.ProductId,
				item.Quantity,
				item.Price
			)).ToList();

			var orderDto = new OrderDto(
				Id: orderId,
				CustomerId: message.CustomerId,
				OrderName: message.UserName,
				ShippingAddress: addressDto,
				BillingAddress: addressDto,
				Payment: paymentDto,
				Status: Ordering.Domain.Enums.OrderStatus.Pending,
				OrderItems: orderItems);
				

			return new CreateOrderCommand(orderDto);
		}
	}
}
