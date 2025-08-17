using Core_Layer.Models.Order;
using Core_Layer.Specefication;
using Core_Layer.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.GenericRepository.SpeceficationRepo
{
	public class OrderSpecefications:Specifications<Order>
	{
		public OrderSpecefications(OrderParams orderParams)
		{
			this.Criteria = O =>
				(orderParams.orderId == null || O.Id == orderParams.orderId) &&
				(string.IsNullOrEmpty(orderParams.PaymentIntentId) || O.PaymentIntentId == orderParams.PaymentIntentId) &&
				(string.IsNullOrEmpty(orderParams.Email) || O.ClientEmail == orderParams.Email);
			
			if (orderParams.orderId != null)
				this.Includes.AddRange(o => o.deliveryMethod, o => o.orderItems);

			if (!string.IsNullOrEmpty(orderParams.Email)&& orderParams.orderId == null)
			{
				this.OrderByDesc = O => O.OrderDate;
				this.Includes.AddRange(o => o.deliveryMethod, o => o.orderItems);

			}
		}
	}
}
