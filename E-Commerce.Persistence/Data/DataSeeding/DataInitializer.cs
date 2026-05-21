using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Entities.ProductModule;
using E_Commerce.Persistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Data.DataSeeding
{
	public class DataInitializer : IDataInitializer
	{
		private readonly StoreDbContext _dbContext;

		public DataInitializer(StoreDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public async Task InitializeAsync()
		{
			var hasProducts = await _dbContext.Products.AnyAsync();
			var hasBrands =	await _dbContext.ProductBrands.AnyAsync();
			var hasTypes = await _dbContext.ProductTypes.AnyAsync();
			//must seed brands and types becuase products depend on them
			try
			{
				if (!hasBrands)
				{

					await LoadDataFromFileAsync<int, ProductBrand>("brands.json", _dbContext.ProductBrands);

				}
				if (!hasTypes)
				{
					await LoadDataFromFileAsync<int, ProductType>("types.json", _dbContext.ProductTypes);

				}
				_dbContext.SaveChanges();
				if (!hasProducts)
				{
					await LoadDataFromFileAsync<int, Product>("products.json", _dbContext.Products);
				}
				_dbContext.SaveChanges();
			}
			catch (Exception ex)
			{

				Console.WriteLine($"Error occured while seeding : {ex}");
			}
		}

		#region Helper Methods

		private async Task LoadDataFromFileAsync<TKey, T>(string fileName, DbSet<T> dbset) where T : BaseEntity<TKey>
		{
			//Another way 
			var filePath = $"..\\E-Commerce.Persistence\\Data\\DataSeeding\\Files\\{fileName}";

			//if data is large will take too much space in memory
			//var Data = File.ReadAllText(filePath);

			if (!File.Exists(filePath)) throw new FileNotFoundException("File is not found : ", fileName);
			try
			{
				using var dataStream = File.OpenRead(filePath);

				var result = await JsonSerializer.DeserializeAsync<List<T>>(dataStream, new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true
				});

				if (result is null || !result.Any()) return;
				//AddRangeAsync will be better if want to call database before adding if identity was created by entity framework

				dbset.AddRange(result!);

			}
			catch (Exception ex)
			{

				Console.WriteLine($"An Error Occured : {ex}");
			}


		}

		#endregion
	}
}
