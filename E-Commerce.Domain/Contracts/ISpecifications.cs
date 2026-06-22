using E_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Contracts
{
	public interface ISpecifications<TEntity,TKey> where TEntity : BaseEntity<TKey>
	{
		//Get only so that whoever uses it wont reinitalize the collection
		public ICollection<Expression<Func<TEntity, object>>> IncludeExpressions { get; }
		
	}
}
