using Core_Layer.Models.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.Interfaces
{
	public interface IPaymentService
	{
		Task<ReturnedBasket> CreateOrUpdatePaymentIntentAsync(string basketId);
		Task OrderPaymentStatusAsync(string PaymentIntentId, bool IsSucceeded);

	}
}
