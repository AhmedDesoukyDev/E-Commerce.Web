
using E_Commerce.Domain.Contracts;
using E_Commerce.Persistence.Data.DataSeeding;
using E_Commerce.Persistence.Data.DbContexts;
using E_Commerce.Web.Extensions;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Web
{
	public class Program
	{
		//Most of Code inside main is implicitly async
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);


			#region Add services to the container.

			builder.AddAppServices();

			#endregion

			var app = builder.Build();

			#region Seeding Data
			await app.InitializeAsync(); //Seeding and Migrations 
			#endregion

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

			//Async here is useless
			//its the last thing to be done
			await app.RunAsync();
		}
	}
}
