using Core_Layer.Specefication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.SpeceficationRepo
{
	public class ProductSpecificationsWithPagination: ProductSpecificationsWithoutPagination
	{

		public ProductSpecificationsWithPagination(ProductFilterParams productFilterParams):base(productFilterParams,true)
		{
			
		}
	}
}
