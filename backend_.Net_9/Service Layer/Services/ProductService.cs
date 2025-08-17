using Core_Layer.Interfaces;
using Core_Layer.Models.Product;
using Core_Layer.Specefication;
using Core_Layer.Specification;
using Repository_Layer.SpeceficationRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Layer.Services
{
	public class ProductService:IProductService // بيتعامل مع الداتا بيز والكنترولر
	{
		private readonly IUnitOfWork _unitOfWork;

		public ProductService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<IReadOnlyList<Product>> GetAllProductsAsyncAfterSpec(ProductFilterParams filterParams)
		{
			var specWithPagination = new ProductSpecificationsWithPagination(filterParams);
			var products = await _unitOfWork.GetRepo<Product>().GetAllAsyncAfterSpec(specWithPagination);
			return products;
		}
		public async Task<int> GetAllProductsCountAsyncAfterSpec(ProductFilterParams filterParams)
		{
			var specWithOutPagination = new ProductSpecificationsWithoutPagination(filterParams);
			var count = await _unitOfWork.GetRepo<Product>().GetCountAsync(specWithOutPagination);
			return count;
		}
		public async Task<Product?> GetProductAfterSpecIdAsync(int id)
		{
			var spec = new ProductSpecificationsWithoutPagination(id);
			var product = await _unitOfWork.GetRepo<Product>().GetAsyncAfterSpecId(spec);
			return product;
		}
		public async Task<IReadOnlyList<Category>> GetAllCategoryAsync()
		{
			var categories = await _unitOfWork.GetRepo<Category>().GetAllAsync();
			return categories;
		}
		public async Task<IReadOnlyList<Brand>> GetAllBrandsAsync()
		{
			var Brands = await _unitOfWork.GetRepo<Brand>().GetAllAsync();
			return Brands;
		}
		public async Task<IReadOnlyList<Product>> GetAllAngularProductsAsync()
		{
			var products = await _unitOfWork.GetRepo<Product>().GetAllAsync();
			return products;
		}
	}
}
