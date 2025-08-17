using Core_Layer.Models.Order;
using Core_Layer.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.Interfaces
{
	public interface IOrderService
	{
		//CreateOrderAsync-GetOrdersForUserAsync-GetOrderByIdForUser-GetDeliveryMethodsAsync

		 Task<Order> CreateOrderAsync(string basketId,ShippingAddress shippingAddress, string clientEmail); // باقي القيم اللي ف الاوردر هجيبها من الباسكيت  عن طريق ال باسكت اي دي 
		 Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string ClientEmail);
		 Task<Order?> GetOrderByOrderIdForUserAsync(string ClientEmail,int orderId);
		 Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();
	}
}
