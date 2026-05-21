using AutoMapper;
using E_Commerce.Domain.Entities.ProductModule;
using E_Commerce.Shared.DTOs.ProductDTOs;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.MappingProfiles
{
	public class ProductPictureUrlResolver : IValueResolver<Product, ProductDTO, string>
	{
		//to get the url from appsetting
		private readonly IConfiguration _configuration;

		public ProductPictureUrlResolver(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public string Resolve(Product source, ProductDTO destination, string destMember, ResolutionContext context)
		{
			//Make sure theres a picture url in src
			if (string.IsNullOrEmpty(source.PictureUrl))
				return string.Empty;

			//Incase the picture is from different source
			if (source.PictureUrl.Contains("http"))
				return source.PictureUrl;

			var BaseUrl = _configuration.GetSection("URLs")["BaseUrl"];
			if (string.IsNullOrEmpty(BaseUrl)) return string.Empty;


			var picUrl = $"{BaseUrl}{source.PictureUrl}";
			return picUrl;

		}
	}
}
