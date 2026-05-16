using E_Commerce.Domain.Entities.ProductModule;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Data.DbContexts
{
	public class StoreDbContext:DbContext
	{
		//so that i dont need to type the connection string in this class
		//i pass the options from the startup class to this class
		//configuration is in appsettings file
		//chaining just to give the dbcontext the options it needs to connect to the database
		public StoreDbContext(DbContextOptions<StoreDbContext> options): base(options) 
		{
			
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

			//in case the configurations are in another assembly than dbcontext
			//modelBuilder.ApplyConfigurationsFromAssembly(typeof(//ClassName).Assembly);
		}

		public DbSet<Product> Products { get; set; }
		public DbSet<ProductBrand> ProductBrands { get; set; }
		public DbSet<ProductType> ProductTypes { get; set; }
	}
}
