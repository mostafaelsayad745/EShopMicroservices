using MassTransit;
using Microsoft.FeatureManagement;

namespace Ordering.Application.Orders.EventHandlers.Domain
{
    public class OrderCreatedEventHandler(IPublishEndpoint publishEndpoint, IFeatureManager featureManager
		, ILogger<OrderCreatedEventHandler> logger)
        : INotificationHandler<OrderCreatedEvent>
    {
        public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain Event Handled : {DomainEvent}", domainEvent.GetType().Name);

			// Check if the OrderFullfilment feature is enabled
			if (await featureManager.IsEnabledAsync("OrderFullfilment"))
            {
				// Publish the order created event to the message broker
				var orderCreatedIntegrationEvent = domainEvent.Order.ToOrderDto();
				await publishEndpoint.Publish(orderCreatedIntegrationEvent, cancellationToken);
			}
		}
    }
}
