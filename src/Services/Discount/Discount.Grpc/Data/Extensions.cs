using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data
{
	public static class Extensions
	{
		// this method is used to auto migrate the database when the application starts
		public static IApplicationBuilder UseMigration(this IApplicationBuilder app)
		{
			using var scope = app.ApplicationServices.CreateScope();
			using var dbContext = scope.ServiceProvider.GetRequiredService<DiscountContext>();
			dbContext.Database.MigrateAsync();

			return app;
		}
	}
}