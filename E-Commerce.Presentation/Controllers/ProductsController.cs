using E_Commerce.Services.Abstraction;
using E_Commerce.Shared.DTOs.ProductDTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly IProductService _productService;

		public ProductsController(IProductService productService)
		{
			_productService = productService;
		}


		[HttpGet]
		// GET : BaseUrl/api/Products
		public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllProducts()
		{
			var products = await _productService.GetAllProductsAsync();
			return Ok(products);
		}
		[HttpGet("types")]
		//GET : BaseUrl/api/Products/types
		public async Task<ActionResult<IEnumerable<TypeDTO>>> GetAllTypes()
		{
			var types = await _productService.GetAllTypesAsync();
			return Ok(types);
		}
		[HttpGet("brands")]
		//GET : BaseUrl/api/Products/brands

		public async Task<ActionResult<IEnumerable<BrandDTO>>> GetAllBrands()
		{
			var brands = await _productService.GetAllBrandsAsync();
			return Ok(brands);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<ProductDTO>> GetProduct(int id) 
		{
			var product = await _productService.GetProductByIdAsync(id);
			return Ok(product);
		}
	}
}
