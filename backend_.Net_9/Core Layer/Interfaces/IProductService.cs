using Core_Layer.Models.Product;
using Core_Layer.Specefication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.Interfaces
{
	public interface IProductService
	{
		Task<IReadOnlyList<Product>> GetAllProductsAsyncAfterSpec(ProductFilterParams filterParams);
		Task<int> GetAllProductsCountAsyncAfterSpec(ProductFilterParams filterParams);
		Task<Product?> GetProductAfterSpecIdAsync(int id);
		Task<IReadOnlyList<Category>> GetAllCategoryAsync();
		Task<IReadOnlyList<Brand>> GetAllBrandsAsync();
		Task<IReadOnlyList<Product>> GetAllAngularProductsAsync();
	}
}
