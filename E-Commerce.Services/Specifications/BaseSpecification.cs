using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Specifications
{
	internal abstract class BaseSpecification<TEntity, TKey> : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
	{
		//object because i dont know what object i'll get from db
		public ICollection<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = []; //Avoid Null

		protected void AddIncludes(Expression<Func<TEntity, object>>includeExp)
		{
			IncludeExpressions.Add(includeExp);
		}
	}
}
