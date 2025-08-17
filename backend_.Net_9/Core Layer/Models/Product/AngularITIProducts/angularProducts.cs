using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.Models.Product
{
	public class angularProducts:BaseClass
	{
		public string name { get; set; } = null!;//
		public int categoryId { get; set; }//
		public decimal price { get; set; }//
		public int quantity { get; set; }
		public string pictureUrl { get; set; } = null!;//
		public Category category { get; set; } = null!;
		public override string ToString()
		{
			return $"name= {name} , categoryId={categoryId}, price={price}, quantity={quantity}, pictureUrl={pictureUrl}";
		}
	}
}
