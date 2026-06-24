using E_Commerce.Domain.Entities.ProductModule;
using E_Commerce.Shared;
using E_Commerce.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Specifications
{
	internal class ProductWithTypesAndBrandSpecifications:BaseSpecification<Product,int>
	{

		public ProductWithTypesAndBrandSpecifications(int id):base(P=>P.Id==id)
		{
			AddIncludes(P => P.ProductType);
			AddIncludes(P => P.ProductBrand);

		}
		public ProductWithTypesAndBrandSpecifications(ProductQueryParams queryParams)
			:base(P=>(!queryParams.BrandId.HasValue || P.BrandId== queryParams.BrandId.Value) &&
			(!queryParams.TypeId.HasValue || P.TypeId== queryParams.TypeId.Value) &&
			(string.IsNullOrEmpty(queryParams.Search) || P.Name.ToLower().Contains(queryParams.Search.ToLower())))
		{
			AddIncludes(P => P.ProductType);
			AddIncludes(P => P.ProductBrand);

			switch (queryParams.Sort)
			{
				case ProductSortingOptions.NameAsc:
					AddOrderBy(P => P.Name);
					break;
				case ProductSortingOptions.NameDesc:
					AddOrderByDesc(P => P.Name);
					break;
				case ProductSortingOptions.PriceAsc:
					AddOrderBy(P => P.Price);
					break;
				case ProductSortingOptions.PriceDesc:
					AddOrderByDesc(P => P.Price);
					break;
				default:
					AddOrderBy(P => P.Id); //For safety
					break;
			}

		}
	}
}
