using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Repositories;
using E_Commerce.Domain.UnitOfWork;
using E_Commerce.Persistence.Data.DbContexts;
using E_Commerce.Persistence.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Data.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly StoreDbContext _dbContext;
		private readonly Dictionary<Type, object> repositories = [];
		public UnitOfWork(StoreDbContext dbContext)
		{
			_dbContext = dbContext;
			
		}
		public async Task<int> CompleteAsync()
		{
			return await _dbContext.SaveChangesAsync();
		}

		public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>, new()
		{
			var EntityType = typeof(TEntity);
			if (repositories.TryGetValue(EntityType, out var repository))
				return (IGenericRepository<TEntity, TKey>)repository;

			var newRepo = new GenericRepository<TEntity, TKey>(_dbContext);
			repositories[EntityType] = newRepo;

			return newRepo;

		}
	}
}
