using AutoMapper;
using Core_Layer.Interfaces;
using Core_Layer.Models.Basket;
using Core_Layer.Models.ErrorsHandle;
using Core_Layer.Models.Order;
using Core_Layer.Models.Product;
using Core_Layer.Specefication;
using Microsoft.Extensions.Configuration;
using Repository_Layer.GenericRepository.SpeceficationRepo;
using Repository_Layer.UnitOfWork;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Layer.Services
{
	public class PaymentService:IPaymentService
	{
		private readonly IBasketService _basketService;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IConfiguration _config;

		public PaymentService(IBasketService basketService,IUnitOfWork unitOfWork,IMapper mapper, IConfiguration config)
		{
			_basketService = basketService;
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_config = config;
		}

		public async Task<ReturnedBasket> CreateOrUpdatePaymentIntentAsync(string basketId)
		{
			//create a payment intent 
			StripeConfiguration.ApiKey = _config["Payment:SecretKey"];

			if (string.IsNullOrEmpty(basketId))
				throw new ThrowException(400, "basketId Is Required!");
			var basket = await _basketService.GetBasketAsync(basketId);
			if (basket.Items == null || !basket.Items.Any())
				throw new ThrowException(400, "Basket has no items.");
			var basketItemsAmount = basket.Items.Sum(item => item.Price * item.Quantity);

			if (basket.DeliveryMethodId == null|| basket.ShippingCost==null)
				throw new ThrowException(400, $"DeliveryMethodId Or ShippingCost Can`t Be Null.!");

		

			var amount = (long)(basketItemsAmount + basket.ShippingCost) * 100;
			var service = new PaymentIntentService();
			if (string.IsNullOrEmpty(basket.PaymentIntentId)) //create paymentIntent
			{
				var options = new PaymentIntentCreateOptions
				{
					Amount = amount,
					Currency = _config["Payment:Currency"] ?? "usd",
					AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
					{
						Enabled = true,
					}
				};
				var paymentIntent = await service.CreateAsync(options);
				basket.PaymentIntentId = paymentIntent.Id; //set payment intent id to the basket
				basket.ClientSecret = paymentIntent.ClientSecret; //set client secret to the basket
				var addBasket = _mapper.Map<AddBasket>(basket); //map ReturnedBasket to AddBasket
				await _basketService.AddOrUpdateBasketAsync(addBasket); //update basket with payment intent id
			}
			else //Update Amount In PaymentIntentCreateOptions //called from OrderService
			{
				var options = new PaymentIntentUpdateOptions
				{
					Amount = amount,
				};
				await service.UpdateAsync(basket.PaymentIntentId, options);
			}

			return basket;

		}
		public async Task OrderPaymentStatusAsync(string PaymentIntentId, bool IsSucceeded)
		{
			// i access it after payment intent is succeeded or failed after Creating Order
			if (string.IsNullOrEmpty(PaymentIntentId))
				throw new ThrowException(400, "PaymentIntentId Is Required!");

			//Get Order With This PaymentIntentId
			var orderParams= new OrderParams
			{
				PaymentIntentId = PaymentIntentId
			};
			var orderSpecs = new OrderSpecefications(orderParams);
			var orderRepo = _unitOfWork.GetRepo<Order>();

			var order = (await orderRepo.GetAsyncAfterSpecId(orderSpecs))!;// i sure that orders have only one order with this spec
			if (IsSucceeded)
			{
				order.orderState = OrderState.PaymentReceived; //set order state to payment received
			}
			else
			{
				order.orderState = OrderState.PaymentFailed; //set order state to payment received
			}
			//update order state in db
			orderRepo.Update(order);      
			await _unitOfWork.CompleteAsync(); //save changes to db 
		}
	}
}
