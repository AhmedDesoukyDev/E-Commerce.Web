using E_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Repositories
{
	public interface IGenericRepository<TEntity,TKey> where TEntity : BaseEntity<TKey> , new()
	{
		Task<IEnumerable<TEntity>> GetAllAsync();

		Task<TEntity?> GetByIdAsync(TKey id);

		Task AddAsync(TEntity entity); //incase we need to call database before adding

		void UpdateAsync(TEntity entity);

		void DeleteAsync(TEntity entity);
	}
}
