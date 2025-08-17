
using Azure;
using Core_Layer.Models.ErrorsHandle;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ReTypeAllByMe.CustomMiddleWare
{
	// دورة حياة الريكويست مهم جدا جدا
	//Http Request
	// لو انا بعتت ريكويست ل اكشن في كونترولر هيمر ال ريكويست ده علي كل الميدلوير اللي عندي وبعدين هيمر ع الكونستركتور بتاع الكنترولر وبعدين هيمر ع الفلتر اللي ع الاكشن وبعدين هيمر ع الاكشن ذات نفسه ولو سجلت خدمة اسكوبد بيبقي ال تايم بتاعها لحد م الريكويست ده يخلص 
	public class HandelErrorMiddleWare : IMiddleware
	{
		private readonly ILogger _logger;
		private readonly IHostEnvironment _hostEnvironment;

		public HandelErrorMiddleWare(ILogger<HandelErrorMiddleWare> logger,IHostEnvironment hostEnvironment )
		{
			_logger = logger;
			_hostEnvironment = hostEnvironment;
		}
		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			// This middleware is intended to handle errors globally in the application.

			try 
			{
				
				// Call the next middleware in the pipeline
				await next(context);

			}
			catch (Exception ex) 
			{
				_logger.LogError(ex, ex.Message);
				if (context.Response.HasStarted)//يجد أنها true، فيسجل تحذير ويرمي الخطأ مرة أخرى:
				{
					_logger.LogWarning("⚠️ The response has already started, the error middleware will not execute.");
					throw;
				}
				var options = new JsonSerializerOptions
				{
					DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
					PropertyNamingPolicy = JsonNamingPolicy.CamelCase // Serialize the error response with camelCase properties for consistency with frontend
				};

				HandleErrors? handleError=null;
				InternalError? internalError = null;

				switch (ex) // Handle specific exceptions if needed
				{
					case ThrowException MythrowException:
						context.Response.StatusCode = MythrowException.Code;
						handleError = new HandleErrors(MythrowException.Code, MythrowException.MyMessage);
						//var handleErrorJson = JsonSerializer.Serialize(handleError, options);
						break;
					default:
						context.Response.StatusCode = StatusCodes.Status500InternalServerError;
						internalError = _hostEnvironment.IsDevelopment() ? new InternalError(ex.Message, ex.StackTrace) : new InternalError("Internal Error Ocuured!");
						//var internalErrorJson = JsonSerializer.Serialize(internalError, options);

						break;
				}
				context.Response.ContentType = "application/json";

				var json = handleError !=null? JsonSerializer.Serialize(handleError, options) : JsonSerializer.Serialize(internalError, options);
				await context.Response.WriteAsync(json);
			}

		}
	}
}
