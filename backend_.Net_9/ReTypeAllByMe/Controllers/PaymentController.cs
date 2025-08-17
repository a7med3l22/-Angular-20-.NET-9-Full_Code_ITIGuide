using Core_Layer.Interfaces;
using Core_Layer.Models.Basket;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service_Layer.Services;
using Stripe;

namespace ReTypeAllByMe.Controllers
{
	public class PaymentController :BaseApiController
	{
		private readonly IPaymentService _paymentService;
		private readonly ILogger<PaymentController> _logger;
		private readonly IConfiguration _configuration;

		public PaymentController(IPaymentService paymentService, ILogger<PaymentController> logger, IConfiguration configuration)
		{
			_paymentService = paymentService;
			_logger = logger;
			_configuration = configuration;
		}
		[HttpGet]
		public async Task<ActionResult<ReturnedBasket>> CreateOrUpdatePaymentIntentAsync(string basketId)
		{
			var basket = await _paymentService.CreateOrUpdatePaymentIntentAsync(basketId);
			return Ok(basket);
		}
		[HttpPost("webhook")]
		public async Task<IActionResult> Webhook()
		{
			var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

			var stripeEvent = EventUtility.ConstructEvent(
				json,
				Request.Headers["Stripe-Signature"],
				_configuration["Payment:WebHookSecretKey"]
			);

			if (stripeEvent.Type == EventTypes.PaymentIntentSucceeded)
			{
				var paymentIntentId = ((PaymentIntent)stripeEvent.Data.Object).Id;
				await _paymentService.OrderPaymentStatusAsync(paymentIntentId, true);
				_logger.LogInformation("✅ PaymentIntent was successful!");
			}
			else if (stripeEvent.Type == EventTypes.PaymentIntentPaymentFailed)
			{
				var paymentIntentId = ((PaymentIntent)stripeEvent.Data.Object).Id;
				await _paymentService.OrderPaymentStatusAsync(paymentIntentId, false);
				_logger.LogInformation("❌ PaymentIntent was failed!");
			}

			return Ok();
		}
	}
}
