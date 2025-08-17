using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.Models.Product.AngularITIProducts
{
	public class angularProductsDto
	{
		public int id { get; set; }//
		public string name { get; set; } = null!;//
		public int categoryId { get; set; }//
		public decimal price { get; set; }//
		public int quantity { get; set; }

		public string? category { get; set; } 

		public string pictureUrl { get; set; } = null!;//

	}
}
