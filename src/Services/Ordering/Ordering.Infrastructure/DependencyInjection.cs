


using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Data;


namespace Ordering.Infrastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
		{
			var connectionString = configuration.GetConnectionString("Database");
			// add services to the container
			services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
			services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
			services.AddDbContext<ApplicationDbContext>((sp,options) =>
			{
				options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
				options.UseSqlServer(connectionString);
			});

			// add this interface to use in the application layer to avoid coupling with the infrastructure layer
			services.AddScoped<IApplicationDbContext, ApplicationDbContext>();			

			return services;
		}
	}
}
