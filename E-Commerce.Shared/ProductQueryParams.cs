using E_Commerce.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shared
{
	public class ProductQueryParams
	{
		public int? TypeId { get; set; }
		public int? BrandId { get; set; }
		public string? Search { get; set; }

		public ProductSortingOptions Sort { get; set; }

	}
}
