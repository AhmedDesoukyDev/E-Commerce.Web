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
		public void Initialize()
		{
			var hasProducts = _dbContext.Products.Any();
			var hasBrands = _dbContext.ProductBrands.Any();
			var hasTypes = _dbContext.ProductTypes.Any();
			//must seed brands and types becuase products depend on them
			try
			{
				if (!hasBrands)
				{

					LoadDataFromFile<int, ProductBrand>("brands.json", _dbContext.ProductBrands);

				}
				if (!hasTypes)
				{
					LoadDataFromFile<int, ProductType>("types.json", _dbContext.ProductTypes);

				}
				_dbContext.SaveChanges();
				if (!hasProducts)
				{
					LoadDataFromFile<int, Product>("products.json", _dbContext.Products);
				}
				_dbContext.SaveChanges();
			}
			catch (Exception ex)
			{

				Console.WriteLine($"Error occured while seeding : {ex}");
			}
		}

		#region Helper Methods

		private void LoadDataFromFile<TKey, T>(string fileName, DbSet<T> dbset) where T : BaseEntity<TKey>
		{
			//Another way 
			var filePath = $"..\\E-Commerce.Persistence\\Data\\DataSeeding\\Files\\{fileName}";

			//if data is large will take too much space in memory
			//var Data = File.ReadAllText(filePath);

			if (!File.Exists(filePath)) throw new FileNotFoundException("File is not found : ", fileName);
			try
			{
				using var dataStream = File.OpenRead(filePath);

				var result = JsonSerializer.Deserialize<List<T>>(dataStream, new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true
				});

				if (result is null || !result.Any()) return;
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
