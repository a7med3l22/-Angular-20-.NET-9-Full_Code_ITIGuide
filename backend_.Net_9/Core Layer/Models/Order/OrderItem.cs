using Core_Layer.Models.Product;

namespace Core_Layer.Models.Order
{
	public class OrderItem:BaseClass
	{
		public ProductItemOrdered productItemOrdered { get; set; } = null!;
		public decimal Price { get; set; }
		public int Quantity { get; set; }

	}
}