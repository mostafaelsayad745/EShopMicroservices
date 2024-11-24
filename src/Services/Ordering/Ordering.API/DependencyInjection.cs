using BuildingBlocks.Exceptions.Handler;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Ordering.Application.Orders.EventHandlers;

namespace Ordering.API
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApiServices(this IServiceCollection services , IConfiguration configuration)
		{
			

			services.AddCarter();

			services.AddExceptionHandler<CustomExceptionHandler>();

			services.AddHealthChecks()
				.AddSqlServer(configuration.GetConnectionString("Database")!);
			return services;
		}

		public static WebApplication UserApiServices(this WebApplication app)
		{

			app.MapCarter();
			app.UseExceptionHandler(options => { });
			app.UseHealthChecks("/health",new 
				HealthCheckOptions
			{
				ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
			});
			return app;
		}
	}
}
