using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.Models.Product.AngularITIProducts
{
	public class UpdateProductAngular
	{


		public string? name { get; set; }  
		public int categoryId { get; set; } 
		public decimal price { get; set; }
		public int quantity { get; set; }
		public  IFormFile? pictureUrl { get; set; }
	}
}
