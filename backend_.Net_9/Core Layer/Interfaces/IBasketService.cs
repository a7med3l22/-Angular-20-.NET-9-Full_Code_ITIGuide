using Core_Layer.Models.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.Interfaces
{
	public interface IBasketService
	{
		 Task<ReturnedBasket> AddOrUpdateBasketAsync(AddBasket basketData);
		 Task<ReturnedBasket?> GetBasketAsync(string id);
		Task<bool> DeleteBasketAsync(string basketId);
	}
}
