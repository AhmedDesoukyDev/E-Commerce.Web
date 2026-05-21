using AutoMapper;
using E_Commerce.Domain.Entities.ProductModule;
using E_Commerce.Shared.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.MappingProfiles
{
	public class ProductProfile:Profile
	{
		public ProductProfile()
		{
			CreateMap<Product, ProductDTO>()
				.ForMember(dest => dest.ProductBrand, opts => opts.MapFrom(src => src.ProductBrand.Name))
				.ForMember(dest => dest.ProductType, opts => opts.MapFrom(src => src.ProductType.Name));

			CreateMap<ProductBrand, BrandDTO>();
			CreateMap<ProductType, TypeDTO>();
		}
	}
}
