using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.Models.Basket
{
	public class ReturnedBasketItem
	{
		public int Id { get; set; } 
		public int Quantity { get; set; }

		// القيم دي هتتساوي ب القيم اللي موجوده في ال برودكت يعني هاخد بس ال id بتاع البرودكت ولما يبقي معايا ال id هجيب القيم دي من ال product repo
		public string ProductName { get; set; } = null!;//
		public decimal Price { get; set; }
		public string PictureUrl { get; set; } = null!;//
		public string Brand { get; set; } = null!;
		public string Category { get; set; } = null!;
	}
}
