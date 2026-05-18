
using E_Commerce.Domain.Contracts;
using E_Commerce.Persistence.Data.DataSeeding;
using E_Commerce.Persistence.Data.DbContexts;
using E_Commerce.Web.Extensions;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Web
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);


			#region Add services to the container.

			builder.AddAppServices();

			#endregion

			var app = builder.Build();
			app.MigratePending().SeedDatabase();

			#region Configure MiddleWares

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers(); //Search for controller with the same name as the request and execute the action method in that controller.
			#endregion

			app.Run();
		}
	}
}
