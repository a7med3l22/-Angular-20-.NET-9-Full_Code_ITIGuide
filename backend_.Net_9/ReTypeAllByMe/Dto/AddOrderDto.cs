using Core_Layer.Models.Order;

namespace ReTypeAllByMe.Dto
{
	public class AddOrderDto
	{
		public string BasketId { get; set; } = null!;
		public ShippingAddress ShippingAddress { get; set; } = null!;

	}
}
