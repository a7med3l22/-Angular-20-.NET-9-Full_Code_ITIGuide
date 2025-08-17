using Core_Layer.Models.Order;
using Core_Layer.Models.Product;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReTypeAllByMe.Dto
{
	public class ReturnOrderDto
	{
		public int Id { get; set; }
		public string ClientEmail { get; set; } = null!;
		public DateTimeOffset OrderDate { get; set; } 
		public string OrderState { get; set; } = null!;
		public ShippingAddress ShippingAddress { get; set; } = null!; 
		public decimal DeliveryMethodCost { get; set; }
		public IReadOnlyList<OrderItemDto> Items { get; set; } = null!;
		public decimal SubTotal { get; set; }
		public decimal Total { get; set; }
	}
}
