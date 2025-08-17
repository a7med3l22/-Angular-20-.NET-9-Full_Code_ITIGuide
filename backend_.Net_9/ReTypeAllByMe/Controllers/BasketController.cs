using Core_Layer.Interfaces;
using Core_Layer.Models.Basket;
using Core_Layer.Models.ErrorsHandle;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ReTypeAllByMe.Controllers
{
	public class BasketController : BaseApiController
	{
		private readonly IBasketService _basketService;

		public BasketController(IBasketService basketService)
		{
			_basketService = basketService;
		}
		//Get Basket By Id
		[HttpGet]
		public async Task<ActionResult<ReturnedBasket>> GetBasket(string id)
		{
			if (string.IsNullOrEmpty(id))
			{
				return BadRequest(new HandleErrors(400, "Basket ID cannot be null or empty."));
			}
			var basket=await _basketService.GetBasketAsync(id);
			return Ok(basket != null ?basket : new ReturnedBasket(id)); //if basket is not null return the basket else return new basket with the id-- الفرونت اند عاوز كده عاوز لو الباسكت فاضيه ترجعله كل اللي ف الباسكت ب القيم الافتراضيه و ال اي دي اللي هيكون ف الباسكت هيساوي  ال اي دي اللي كان بيبحث عنه
		}
		//AddOrUpdate Basket
		[HttpPost]
		public async Task<ActionResult<ReturnedBasket>> AddOrUpdateBasket(AddBasket basket)
		{
			if (basket is null || string.IsNullOrEmpty(basket.Id) || basket.Items.Count == 0)
				return BadRequest("Basket Or basket ID Or basket Items Cant be null.");

			var updatedBasket = await _basketService.AddOrUpdateBasketAsync(basket);
			if (updatedBasket == null)
			{
				return BadRequest(new HandleErrors(400, "Failed to add or update the basket."));
			}
			return Ok(updatedBasket);
		}
		//Delete Basket
		[HttpDelete]
		public async Task<ActionResult<bool>> DeleteBasket(string id)
		{
			if (string.IsNullOrEmpty(id))
			{
				return BadRequest(new HandleErrors(400, "Basket ID cannot be null or empty."));
			}
			var isDeleted = await _basketService.DeleteBasketAsync(id);
			if (!isDeleted)
			{
				return NotFound(new HandleErrors(404, "Basket not found for deletion."));
			}
			return Ok(isDeleted);
		}

	}
}
