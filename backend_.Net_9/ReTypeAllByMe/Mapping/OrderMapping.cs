using AutoMapper;
using Core_Layer.Models.Basket;
using Core_Layer.Models.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReTypeAllByMe.Dto;

namespace ReTypeAllByMe.Mapping
{
	public class OrderMapping : Profile
	{
		public OrderMapping()
		{
			CreateMap<ReturnedBasketItem, OrderItem>()
				.ForMember(dest => dest.productItemOrdered, opt => opt.MapFrom(src => src))  // الشرح تحت خالص 
				.ForMember(dest=>dest.Id,opt=>opt.Ignore()); // اكني بقوله فكك منها اكنها مش موجوده 


			CreateMap<ReturnedBasketItem, ProductItemOrdered>()
				.ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id))
				.ForMember(dest => dest.PictureUrl, opt => opt.MapFrom(src => src.PictureUrl))
				.ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName));



			CreateMap<Order, ReturnOrderDto>()
				.ForMember(dest => dest.DeliveryMethodCost, opt => opt.MapFrom(src => src.deliveryMethod != null ? src.deliveryMethod.Cost : 0))
				.ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.orderItems))
				//.ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.GetTotal)) // مش لازم اعمل دي هو هياخد القيمة من جيت توتال هيحططها ف ال توتال ع طول هو هيفهم ده لواحده
				;
			

			CreateMap<OrderItem, OrderItemDto>()
				.ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id))
				.ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.productItemOrdered.ProductName))
				.ForMember(dest => dest.PictureUrl, opt => opt.MapFrom(src => src.productItemOrdered.PictureUrl));


		}
	}
}

/*
 وممكن دي 
CreateMap<ReturnedBasketItem, OrderItem>()
				.ForMember(dest => dest.productItemOrdered, opt => opt.MapFrom(src =>
				new ProductItemOrdered
				{
					PictureUrl=src.PictureUrl,
					ProductName=src.ProductName,
					ProductId=src.Id
				}
				));

			/*
					اكني عملت كده ب الظبط 

			 dest.productItemOrdered= new ProductItemOrdered
				{
					PictureUrl=src.PictureUrl,
					ProductName=src.ProductName,
					ProductId=src.Id
				}
			 
			 */


// اكني بقوله خد القيم اللي ف ال ReturnedBasketItem حطها في ProductItemOrdered وعلمته اازاي ياخد منه عن طريق 	CreateMap<ReturnedBasketItem, ProductItemOrdered>()
/*
	كيف يعمل أوتو مابر هنا؟
	CreateMap<ReturnedBasketItem, OrderItem>()
		.ForMember(dest => dest.productItemOrdered, opt => opt.MapFrom(src => src));
	فهو يفهم:

	"لملء productItemOrdered من src، ابحث عن ماب موجود من نوع ReturnedBasketItem إلى نوع ProductItemOrdered."

	✅ وبالتالي يقوم بتحويل src إلى ProductItemOrdered تلقائيًا باستخدام الماب الذي كتبته مسبقًا:

	CreateMap<ReturnedBasketItem, ProductItemOrdered>()
 */
