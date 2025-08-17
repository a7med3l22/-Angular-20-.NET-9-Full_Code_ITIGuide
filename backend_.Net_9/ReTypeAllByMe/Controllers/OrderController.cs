using AutoMapper;
using Core_Layer.Interfaces;
using Core_Layer.Models.ErrorsHandle;
using Core_Layer.Models.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReTypeAllByMe.Dto;
using System.Security.Claims;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ReTypeAllByMe.Controllers
{
	[Authorize]
	public class OrderController : BaseApiController
	{
		private readonly IOrderService _orderService;
		private readonly IMapper _mapper;

		public OrderController(IOrderService orderService,IMapper mapper)
		{
			_orderService = orderService;
			_mapper = mapper;
		}
		[ProducesResponseType(typeof(ReturnOrderDto), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(HandleErrors), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(HandleErrors), StatusCodes.Status401Unauthorized)]
		[HttpPost]
		public async Task<ActionResult<ReturnOrderDto>> CreateOrderAsync(AddOrderDto addOrderDto)
		{
			if (addOrderDto == null || addOrderDto.ShippingAddress == null || string.IsNullOrEmpty(addOrderDto.BasketId))
				return BadRequest(new HandleErrors(400,"Order Or Address Or BasketId Cant Be Null"));

			var ClientEmail= GetEmail();
			var order=await _orderService.CreateOrderAsync(addOrderDto.BasketId, addOrderDto.ShippingAddress, ClientEmail);

			var returnedOrder = _mapper.Map<ReturnOrderDto>(order);
			return Ok(returnedOrder);
		}
		[ProducesResponseType(typeof(ReturnOrderDto), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(HandleErrors), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(HandleErrors), StatusCodes.Status401Unauthorized)]

		[HttpGet("{orderId}")]
		public async Task<ActionResult<IReadOnlyList<ReturnOrderDto>>> GetOrderByOrderIdForUserAsync(int orderId)
		{
			var ClientEmail = GetEmail();

			var order =await _orderService.GetOrderByOrderIdForUserAsync(ClientEmail, orderId);
			if (order == null)
				return NotFound(new HandleErrors(404, "Not Found Order"));
			var returnedOrder = _mapper.Map<ReturnOrderDto>(order);
			return Ok(returnedOrder);
		}
		[ProducesResponseType(typeof(HandleErrors), StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(typeof(IReadOnlyList<ReturnOrderDto>), StatusCodes.Status200OK)]
		[HttpGet]
		public async Task<ActionResult<IReadOnlyList<ReturnOrderDto>>?> GetOrdersForUserAsync()
		{
			var ClientEmail = GetEmail();
			var orders = await _orderService.GetOrdersForUserAsync(ClientEmail);
			if(orders.Count==0)
				return null;
			var returnedOrders = _mapper.Map<IReadOnlyList<ReturnOrderDto>>(orders);
			return Ok(returnedOrders);
		}
		[ProducesResponseType(typeof(IReadOnlyList<DeliveryMethod>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(HandleErrors), StatusCodes.Status404NotFound)]
		[HttpGet("DeliveryMethods")]
		public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
		{
			var DeliveryMethods = await _orderService.GetDeliveryMethodsAsync();
			if (DeliveryMethods.Count == 0) { return NotFound(new HandleErrors(404, "not found any DeliveryMethod")); }
			return Ok(DeliveryMethods);
		}

		private string GetEmail()
		{
			var email =User.FindFirstValue(ClaimTypes.Email);
			if (string.IsNullOrEmpty(email))
				throw new ThrowException(404,"No email found in claims!"); //Good
			return email;
		}

	}

}
