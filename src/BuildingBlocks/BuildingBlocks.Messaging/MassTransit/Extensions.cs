using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BuildingBlocks.Messaging.MassTransit
{
	public static class Extensions
	{
		// Extension method to add message broker services to the IServiceCollection
		public static IServiceCollection AddMessageBroker(this IServiceCollection services, IConfiguration configuration, Assembly? assembly = null)
		{
			// Add MassTransit services to the IServiceCollection
			services.AddMassTransit(config =>
			{
				// Set endpoint name formatter to use kebab-case
				config.SetKebabCaseEndpointNameFormatter();

				// If an assembly is provided, add consumers from that assembly
				if (assembly is not null)
					config.AddConsumers(assembly);

				// Configure MassTransit to use RabbitMQ
				config.UsingRabbitMq((context, configurator) =>
				{
					// Set the RabbitMQ host using the configuration values
					configurator.Host(new Uri(configuration["MessageBroker:Host"]!), host =>
					{
						// Set the username for RabbitMQ
						host.Username(configuration["MessageBroker:Username"]);
						// Set the password for RabbitMQ
						host.Password(configuration["MessageBroker:Password"]);
					});
					// Configure endpoints for the RabbitMQ host
					configurator.ConfigureEndpoints(context);
				});
			});
			// Return the IServiceCollection to allow for method chaining
			return services;
		}
	}
}
