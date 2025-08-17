using Core_Layer.Models.Product;
using Core_Layer.Specification;
using Repository_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.Specefication
{
	public class ProductSpecificationsWithoutPagination : Specifications<Product>
	{
		public ProductSpecificationsWithoutPagination(int id)
		{
			this.Criteria = p => p.Id == id; // filter by Id
			this.include(); // include Brand and Category
		}

		// i want to send to createria [sort,categoryId,brandId,PageSize,PageIndex,Search] 
		public ProductSpecificationsWithoutPagination(ProductFilterParams productFilterParams,bool IsPagination=false)
		{
			

			this.Criteria = p => (productFilterParams.brandId == null || p.Brand.Id == productFilterParams.brandId)
							   &&(productFilterParams.categoryId == null || p.Category.Id == productFilterParams.categoryId)
							   &&(string.IsNullOrEmpty(productFilterParams.Search) || p.Name.ToLower().Contains(productFilterParams.Search.ToLower()))
							   ;
			if (IsPagination)
			{
				this.IsPagingEnabled = true;
				this.Take = productFilterParams.PageSize; // default value is 6
				this.Skip = (productFilterParams.PageIndex - 1) * (productFilterParams.PageSize); // default value is 1, min value is 1

				if (string.IsNullOrEmpty(productFilterParams.sort))
				{
					this.OrderBy = p => p.Name; // default value is Name
				}
				else
				{
					switch (productFilterParams.sort.ToLower())
					{
						case "name":
							this.OrderBy = p => p.Name;
							break;
						case "namedesc":
							this.OrderByDesc = p => p.Name;
							break;
						case "price":
							this.OrderBy = p => p.Price;
							break;
						case "pricedesc":
							this.OrderByDesc = p => p.Price;
							break;
						case "id":
							this.OrderBy = p => p.Id;
							break;
						case "iddesc":
							this.OrderByDesc = p => p.Id;
							break;
						default:
							this.OrderBy = p => p.Name; // default value is Name
							break;
					}
				}
				this.include(); // include Brand and Category
			}
		}


		private void include()
		{
			this.Includes.Add(p => p.Brand); // include Brand
			this.Includes.Add(p => p.Category); // include Category
		}

	}
}
