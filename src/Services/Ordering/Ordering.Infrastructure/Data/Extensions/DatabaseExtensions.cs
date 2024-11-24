
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

namespace Ordering.Infrastructure.Data.Extensions
{
	public static class DatabaseExtensions
	{
		public async static Task InitializeDatabaseAsync(this WebApplication app)
		{
			using var scope = app.Services.CreateScope();

			var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

			context.Database.MigrateAsync().GetAwaiter().GetResult();

			await SeedAsync(context);
		}

		private static async Task SeedAsync(ApplicationDbContext context)
		{
			await SeedCustomerAsyc(context);
			await SeedProductAsyc(context);
			await SeedOrderWithItmesAsyc(context);

		}

		private static async Task SeedOrderWithItmesAsyc(ApplicationDbContext context)
		{
			if (! await context.Orders.AnyAsync())
			{
				await context.Orders.AddRangeAsync(InitialData.OrdersWithItems);
				await context.SaveChangesAsync();
			}
		}

		private static async Task SeedProductAsyc(ApplicationDbContext context)
		{
			if (! await context.Products.AnyAsync())
			{
				await context.Products.AddRangeAsync(InitialData.Products);

				await context.SaveChangesAsync();
			}
		}

		private static async Task SeedCustomerAsyc(ApplicationDbContext context)
		{
			if (! await context.Customers.AnyAsync())
			{
				await context.Customers.AddRangeAsync(InitialData.Customers);
				await context.SaveChangesAsync();
			}
		}
	}
}
