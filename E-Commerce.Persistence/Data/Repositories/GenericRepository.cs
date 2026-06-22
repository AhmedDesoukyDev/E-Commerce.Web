using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Repositories;
using E_Commerce.Persistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace E_Commerce.Persistence.Data.Repositories
{
	public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>, new()
	{
		private readonly StoreDbContext _dbContext;

		public GenericRepository(StoreDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public async Task AddAsync(TEntity entity)=>await _dbContext.Set<TEntity>().AddAsync(entity);
		

		public void DeleteAsync(TEntity entity)=>_dbContext.Set<TEntity>().Remove(entity);
		

		public async Task<IEnumerable<TEntity>> GetAllAsync(/*Expression<Func<TEntity,bool>> condition = null!*/)
			=> await _dbContext.Set<TEntity>().ToListAsync();

		public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> specifications)
		{
			
			return  await SpecificationEvaluator.CreateQuery(_dbContext.Set<TEntity>(), specifications).ToListAsync();
		}

		public async Task<TEntity?> GetByIdAsync(TKey id)=>await _dbContext.Set<TEntity>().FindAsync(id);
		

		public void UpdateAsync(TEntity entity) => _dbContext.Set<TEntity>().Update(entity);
	
	}
}
