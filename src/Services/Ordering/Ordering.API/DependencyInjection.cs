namespace Ordering.API
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApiServices(this IServiceCollection services)
		{
			return services;
		}

		public static WebApplication UserApiServices(this WebApplication app)
		{
			return app;
		}
	}
}
