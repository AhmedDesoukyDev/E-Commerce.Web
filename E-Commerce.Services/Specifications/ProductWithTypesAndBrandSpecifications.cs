using E_Commerce.Domain.Entities.ProductModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Specifications
{
	internal class ProductWithTypesAndBrandSpecifications:BaseSpecification<Product,int>
	{
		public ProductWithTypesAndBrandSpecifications()
		{
			AddIncludes(P => P.ProductType);
			AddIncludes(P => P.ProductBrand);
		}
	}
}
