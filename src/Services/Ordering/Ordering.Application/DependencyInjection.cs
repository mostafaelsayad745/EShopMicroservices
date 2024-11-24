


using BuildingBlocks.Messaging.MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.FeatureManagement;

namespace Ordering.Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services , IConfiguration configuration)
		{
			// Add services to the container
			// add mediator
			services.AddMediatR(config =>
			{
				config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
				config.AddOpenBehavior(typeof(LoggingBehavior<,>));
				config.AddOpenBehavior(typeof(ValidationBehavior<,>));
			});

			// add validators
			services.AddFeatureManagement();

			// add message broker
			services.AddMessageBroker(configuration, Assembly.GetExecutingAssembly());
			return services;
		}
	}
}
