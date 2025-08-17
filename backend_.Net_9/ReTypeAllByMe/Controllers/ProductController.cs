using AutoMapper;
using Core_Layer.Interfaces;
using Core_Layer.Models.ErrorsHandle;
using Core_Layer.Models.Product;
using Core_Layer.Models.Product.AngularITIProducts;
using Core_Layer.Specefication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ReTypeAllByMe.Attributes;
using ReTypeAllByMe.Dto;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ReTypeAllByMe.Controllers
{
	// مهم جدا // أي حاجة  استاتيك بتتتحمل ف الذاكرة اول م يتم استخدامه وبيخليه عايش طو لعمر التطبيق
	public class ProductController : BaseApiController
	{
		private readonly IProductService _productService;
		private readonly IMapper _mapper;

		public ProductController(IProductService productService, IMapper mapper)
		{
			_productService = productService;
			_mapper = mapper;
		}
		//get All Products
		[Cache(4)]
		[ProducesResponseType(type:typeof(ReturnedResponse),statusCode:StatusCodes.Status200OK)]//for swagger documentation	
		[HttpGet]
		public async Task<ActionResult<ReturnedResponse>> GetAllProducts([FromQuery] ProductFilterParams filterParams)  // عملت فروم كويري لان  ProductFilterParams عبارة عن كلاس 
																														// ف الكلاس طبيعته بيتبعت ف ال بودي ف ال اسوجر هيفتكر اني هبعتها ف ال بودي لانها كلاس وال جيت مش بتقرأ من ال بودي ف عملت ف روم كويري علشان اعرف ال سواجر ان القيم هتتبعت ف الكويري
		{

			var products = await _productService.GetAllProductsAsyncAfterSpec(filterParams);
			var ProductsDto= _mapper.Map<IReadOnlyList<ProductDto>>(products);
			var count=await _productService.GetAllProductsCountAsyncAfterSpec(filterParams);
			var returnedResponse = new ReturnedResponse
			{
				PageIndex=filterParams.PageIndex,
				PageSize=filterParams.PageSize,
				Count = count,
				Data = ProductsDto
			};
			return Ok(returnedResponse);
		}
		
		//get Product By Id
		[HttpGet("{id}")]
		public async Task<ActionResult<ProductDto>> GetProductById(int id)
		{	
			var product = await _productService.GetProductAfterSpecIdAsync(id);
			if (product == null) return NotFound(new HandleErrors(400,"not found any product!"));
			var productDto = _mapper.Map<ProductDto>(product);
			return Ok(productDto);
		}
		//Get Categories
		[HttpGet("Categories")]
		public async Task<ActionResult<IReadOnlyList<Category>>> GetAllCategories()
		{
			var categories = await _productService.GetAllCategoryAsync();
			if (categories.Count == 0) return NotFound(new HandleErrors(404, "not found any categories!"));
			return Ok(categories);//Or //return categories;
		}
		[HttpGet("Brands")]
		public async Task<ActionResult<IReadOnlyList<Brand>>> GetAllBrands()
		{
			var Brands = await _productService.GetAllBrandsAsync();
			if (Brands.Count == 0) return NotFound(new HandleErrors(404, "not found any Brands!"));
			return Ok(Brands);//Or //return Brands;
		}
		

		//[HttpGet("addProduct")]
		//public async Task  GetAllBrands(int categoryId, int brandId)
		//{
		//	await unitOfWork.AddProduct(categoryId, brandId);
		//	return;
		//}
	}
}
