using Core_Layer.Models.Order;
using Core_Layer.Models.Product;
using Core_Layer.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.Specefication
{
	public class AngularITICategorySpec : Specifications<Category>
	{
		public AngularITICategorySpec(FilterAndSortCategories filterAndSort)
		{

			Criteria = c =>
				string.IsNullOrWhiteSpace(filterAndSort.category) || c.Name.ToLower().Contains(filterAndSort.category.ToLower());

			if (filterAndSort.SortBy != null)
			{
				switch (filterAndSort.SortBy.Trim().ToLower())
				{
					case "name":
						OrderBy = c => c.Name;
						break;
					case "namedesc":
						OrderByDesc = c => c.Name;
						break;
					default:
						OrderBy = c => c.Name;
						break;

				}

			}
		}

	}
}
