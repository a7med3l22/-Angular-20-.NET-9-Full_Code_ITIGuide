using Core_Layer.Models.Product;
using Core_Layer.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.Specefication
{
	public class AngularProductITISpec : Specifications<angularProducts>
	{
		public AngularProductITISpec()
		{
			Includes.Add(p => p.category);
		}
		public AngularProductITISpec(FilterAndSortProduct filterAndSort)
		{
			Includes.Add(p => p.category);

			Criteria = p =>
				(string.IsNullOrWhiteSpace(filterAndSort.name) || p.name.ToLower().Contains(filterAndSort.name.ToLower())) &&
				(!filterAndSort.categoryId.HasValue || p.categoryId == filterAndSort.categoryId.Value) &&
				(string.IsNullOrWhiteSpace(filterAndSort.category) || p.category.Name.ToLower().Contains(filterAndSort.category.ToLower()));


			if (filterAndSort.SortBy != null)
			{
				switch (filterAndSort.SortBy.Trim().ToLower())
				{
					case "name":
						OrderBy = p => p.name;
						break;
					case "namedesc":
						OrderByDesc = p => p.name;
						break;
					case "price":
						OrderBy = p => p.price;
						break;
					case "pricedesc":
						OrderByDesc = p => p.price;
						break;
					case "quentity":
						OrderBy = p => p.quantity;
						break;
					case "quentitydesc":
						OrderByDesc = p => p.quantity;
						break;
					default:
						OrderBy = p => p.name;
						break;

				}

			}
		}


		
	}
}
