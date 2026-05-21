using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.UnitOfWork
{
	public interface IUnitOfWork
	{
		Task<int> CompleteAsync();

		IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>, new();

	}
}
