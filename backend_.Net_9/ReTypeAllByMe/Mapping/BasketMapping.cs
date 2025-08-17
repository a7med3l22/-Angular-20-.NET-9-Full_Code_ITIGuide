using AutoMapper;
using Core_Layer.Models.Basket;

namespace ReTypeAllByMe.Mapping
{
	public class BasketMapping:Profile
	{
		public BasketMapping() {
			CreateMap<AddBasket, ReturnedBasket>()// الحاجات اللي جوا ال ادد باسكت حطها جوه ال ريتيرندباسكت
			.ForMember(dest => dest.Items, opt => opt.Ignore()) // هيتجاهل عند التحويل من AddBasket إلى ReturnedBasket فقط ال Items
			.ReverseMap();

			CreateMap<ReturnedBasketItem, AddBasketItem>();

		}
	}
}
