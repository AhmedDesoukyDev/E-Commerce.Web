using E_Commerce.Domain.Contracts;
using E_Commerce.Persistence.Data.DataSeeding;
using E_Commerce.Persistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Web.Extensions
{
	public static class WebApplicationExtensions
	{
		public static WebApplication MigratePending(this WebApplication app)
		{
			using var scope = app.Services.CreateScope();
			var dbContext = scope.ServiceProvider.GetRequiredService<StoreDbContext>();
			if (dbContext.Database.GetPendingMigrations().Any()) dbContext.Database.Migrate();

			return app;


		}

		public static WebApplication SeedDatabase(this WebApplication app)
		{
			using var scope = app.Services.CreateScope();
			var dataInitializer = scope.ServiceProvider.GetRequiredService<IDataInitializer>();
			dataInitializer.Initialize();
			return app;

		}
	}
}
