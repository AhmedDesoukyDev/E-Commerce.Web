using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.UnitOfWork;
using E_Commerce.Persistence.Data.DataSeeding;
using E_Commerce.Persistence.Data.DbContexts;
using E_Commerce.Persistence.Data.UnitOfWork;
using E_Commerce.Services;
using E_Commerce.Services.Abstraction;
using E_Commerce.Services.MappingProfiles;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Web.Extensions
{
	public static class WebApplicationBuilderExtensions
	{
		public static WebApplicationBuilder AddAppServices(this WebApplicationBuilder builder)
		{


			builder.Services.AddControllers();
			//For swagger
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddDbContext<StoreDbContext>(
				options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
			);
			builder.Services.AddScoped<IDataInitializer, DataInitializer>();
			builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
			builder.Services.AddAutoMapper(typeof(ServiceLayerReference).Assembly);
			builder.Services.AddScoped<IProductService, ProductService>();
			return builder;

		}
	}
}
