using E_Commerce.Domain.Contracts;
using E_Commerce.Persistence.Data.DataSeeding;
using E_Commerce.Persistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Web.Extensions
{
	public static class WebApplicationExtensions
	{
		public static async Task<WebApplication> MigratePendingAsync(this WebApplication app)
		{
			await using var scope = app.Services.CreateAsyncScope();
			var dbContext = scope.ServiceProvider.GetRequiredService<StoreDbContext>();
			var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();
			if (pendingMigrations.Any()) await dbContext.Database.MigrateAsync();

			return app;


		}

		public static async Task<WebApplication> SeedDatabaseAsync(this WebApplication app)
		{
			//AsyncScope here is a scope that work asynchronously
			await using var scope = app.Services.CreateAsyncScope();
			var dataInitializer = scope.ServiceProvider.GetRequiredService<IDataInitializer>();
			await dataInitializer.InitializeAsync();
			return app;

		}
		public static async Task<WebApplication> InitializeAsync(this WebApplication app)
		{
			await app.MigratePendingAsync();
			await app.SeedDatabaseAsync();
			return app;
		}
	}
}
