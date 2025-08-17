using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.Models.Basket
{
	public class ReturnedBasket
	{
		public string Id { get; set; }=null!;
		public List<ReturnedBasketItem> Items { get; set; } = new();
		public string? ClientSecret { get; set; }
		public string? PaymentIntentId { get; set; }
		public int? DeliveryMethodId { get; set; }
		public decimal? ShippingCost { get; set; }
		public ReturnedBasket(string id)
		{
			Id = id;
		}
	}
}
