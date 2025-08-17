using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.Models.Product
{
	public class ProductDto
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;
		public string Description { get; set; } = null!;
		public decimal Price { get; set; }
		public string PictureUrl { get; set; } = null!;
		public string Category { get; set; } = null!;
		public int CategoryId { get; set; }
		public int BrandId { get; set; }
		public string ProductBrand { get; set; } = null!;
	}
}
