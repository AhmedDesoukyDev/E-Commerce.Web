using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence
{
	internal static class SpecificationEvaluator
	{
		//DRY CONCEPT
		public static IQueryable<TEntity> CreateQuery<TEntity,TKey>(IQueryable<TEntity> EntryPoint,
			ISpecifications<TEntity,TKey> specifications) where TEntity :BaseEntity<TKey>
		{
			IQueryable<TEntity> Query = EntryPoint;
			if(specifications is not null )
			{
				if(specifications.Criteria is not null)
				{
					Query = Query.Where(specifications.Criteria);
				}
				if(specifications.IncludeExpressions is not null && specifications.IncludeExpressions.Any())
				{
					//Instead of foreach
					Query = specifications.IncludeExpressions.Aggregate(Query, (currentQuery, IncludeExp) => currentQuery.Include(IncludeExp));

				}
				if(specifications.OrderBy is not null)
				{
					Query=Query.OrderBy(specifications.OrderBy);
				}
				if(specifications.OrderByDesc is not null)
				{
					Query = Query.OrderByDescending(specifications.OrderByDesc);
				}
			}
			return Query;

		}
	}
}
