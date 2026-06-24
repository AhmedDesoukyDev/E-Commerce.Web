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
		//Filteration
		public Expression<Func<TEntity, bool>> Criteria { get; }
		protected BaseSpecification(Expression<Func<TEntity, bool>> criteria)
		{
			Criteria = criteria;
		}
		public ICollection<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = []; //Avoid Null

		public Expression<Func<TEntity, object>> OrderBy { get; private set; }

		public Expression<Func<TEntity, object>> OrderByDesc { get; private set; }


		//i set them via method to be optional for whoever uses specification
		protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExp)
		{
			OrderBy = orderByExp;
		}
		protected void AddOrderByDesc(Expression<Func<TEntity, object>> orderByDescExp)
		{
			OrderByDesc = orderByDescExp;
		}
		protected void AddIncludes(Expression<Func<TEntity, object>>includeExp)
		{
			IncludeExpressions.Add(includeExp);
		}
	}
}
