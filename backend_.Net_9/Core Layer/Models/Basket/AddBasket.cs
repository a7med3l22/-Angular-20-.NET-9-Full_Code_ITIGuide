using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.Models.Basket
{
	public class AddBasket
	{
		public string Id { get; set; }
		public List<AddBasketItem> Items { get; set; } = new();
		public string? ClientSecret { get; set; }
		public string? PaymentIntentId { get; set; } 
		public int? DeliveryMethodId { get; set; }
		public AddBasket(string id)
		{
			Id = id;
		}
	}
}
