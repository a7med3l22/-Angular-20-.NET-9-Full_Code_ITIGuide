using AutoMapper;
using Core_Layer.Interfaces;
using Core_Layer.Models.Basket;
using Core_Layer.Models.ErrorsHandle;
using Core_Layer.Models.Order;
using Core_Layer.Models.Product;
using Core_Layer.Specefication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Repository_Layer.GenericRepository.Data;
using Repository_Layer.GenericRepository.SpeceficationRepo;
using System.Collections.Generic;

namespace Service_Layer.Order_Service
{
	[Authorize]
	public class OrderService : IOrderService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IBasketService _basketService;
		private readonly IMapper _mapper;
		private readonly IPaymentService _paymentService;

		public OrderService(IUnitOfWork unitOfWork,IBasketService basketService,IMapper mapper,IPaymentService paymentService) 
		{
			_unitOfWork = unitOfWork;
			_basketService = basketService;
			_mapper = mapper;
			_paymentService = paymentService;
		}
		// بدل م ترجع نل للكنترولي ارمي اكسبشن طالما انتا مهندل الميدل وير throw new ArgumentException(""); بدل null !

		public async Task<Order> CreateOrderAsync(string basketId, ShippingAddress shippingAddress, string clientEmail)
		{
			//Get Basket By BasketId 
			//orderState-deliveryMethod-orderItems-SubTotal-PaymentIntentId

			if (shippingAddress == null || string.IsNullOrEmpty(basketId) || string.IsNullOrEmpty(clientEmail))
				throw new ThrowException(400,"Invalid input: shippingAddress is null or BasketId/ClientEmail is empty.");

			var basket = await _basketService.GetBasketAsync(basketId);

			if(basket == null|| basket.Items.Count==0|| basket.DeliveryMethodId==null|| string.IsNullOrEmpty(basket.PaymentIntentId))
				throw new ThrowException(400, "Invalid basket: basket is null, has no items, missing delivery method, or missing payment intent.");
			var orderRepo = _unitOfWork.GetRepo<Order>();
			//<<check if there is any order that have same PaymentIntentId or not>>
			var orderParams = new OrderParams { PaymentIntentId = basket.PaymentIntentId }; 
			var orderSpec = new OrderSpecefications(orderParams);
			var ordersHaveThisPaymentIntentId =await orderRepo.GetAllAsyncAfterSpec(orderSpec);
			if(ordersHaveThisPaymentIntentId.Count!=0)
			{
				orderRepo.RemoveRange(ordersHaveThisPaymentIntentId);
				await _paymentService.CreateOrUpdatePaymentIntentAsync(basketId); //To Update Amount In PaymentIntentCreateOptions // go to Payment Service
			}

			                 
			var orderItems = _mapper.Map<ICollection<OrderItem>>(basket.Items);
			var deliveryMethod=await _unitOfWork.GetRepo<DeliveryMethod>().GetAsyncById(basket.DeliveryMethodId.Value);
			if (deliveryMethod == null)
				throw new ThrowException(400, "Invalid delivery method: delivery method not found for the provided ID.");
			var subTotal = basket.Items.Sum(BI=>BI.Price*BI.Quantity);

			var order = new Order
			{
				orderItems = orderItems,
				deliveryMethod=deliveryMethod,
				ClientEmail=clientEmail,
				SubTotal= subTotal,
				PaymentIntentId=basket.PaymentIntentId,
				shippingAddress=shippingAddress
			};
			//add Order to Db
			 orderRepo.Add(order);

			//SaveChanges to Db
			if (await _unitOfWork.CompleteAsync()<= 0) //
				throw new ThrowException(500, "Failed to create the order due to a database error.");

			return order;
		}

		public Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()=> _unitOfWork.GetRepo<DeliveryMethod>().GetAllAsync();


		public async Task<Order?> GetOrderByOrderIdForUserAsync(string ClientEmail, int orderId)
		{
			if (string.IsNullOrEmpty(ClientEmail))
			{
				throw new ThrowException(400, "Invalid input: ClientEmail is null.");
			}
			var orderParams=new OrderParams { Email=ClientEmail,orderId=orderId };
			var orderSpec = new OrderSpecefications(orderParams);
			var order=await _unitOfWork.GetRepo<Order>().GetAsyncAfterSpecId(orderSpec);
			return order;

		}

		public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string ClientEmail)
		{
			if (string.IsNullOrEmpty(ClientEmail))
			{
				throw new ArgumentException("Invalid input: ClientEmail is null.");
			}
			var orderParams = new OrderParams { Email = ClientEmail };
			var orderSpec = new OrderSpecefications(orderParams);
			var orders =await _unitOfWork.GetRepo<Order>().GetAllAsyncAfterSpec(orderSpec);
			return orders;
		}
	}
}
/*
 لو انا معملتش كده 
if (!keyValuePairs.ContainsKey(type))
    keyValuePairs[type] = new GenericRepository<T>(_dbContext);

return (IGenericRepository<T>)keyValuePairs[type];

وعملت كده ع طول 
return new GenericRepository<T>(_dbContext);

وجيت ف السيرفس مثلا وعملت كده 
var orderRepo = _unitOfWork.GetRepo<Order>();

وبعد كده استخدمت النسخة دي orderRepo  طول السيرفس 
ف مش كده اي حاجة هعملها ف السيرفس هتفضل محفوظه داخل orderRepo   ومش هتعمل  new GenericRepository<T>(_dbContext); 
م الاول لاني مستدعتش ال  _unitOfWork.GetRepo<Order>(); غير مرة واحده!
 ✅ كلامك هنا صحيح بنسبة 100%
 
 */

