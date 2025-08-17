
using Core_Layer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System.Text.Json;

namespace ReTypeAllByMe.Attributes
{
	public class CacheAttribute : Attribute, IAsyncActionFilter
	{
		private readonly int _timeLifeInDays=5;

		public CacheAttribute(int timeLifeInDays)
		{
			_timeLifeInDays = timeLifeInDays;
		}
		public async Task  OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			var cacheKey=GetCacheKey(context);

			var cacheRepo= context.HttpContext.RequestServices.GetRequiredService<ICacheRepo>();

			var getCache = await cacheRepo.getCacheInRedis(cacheKey);
			if(getCache==null)
			{
				var excutedAction = await next.Invoke(); // هينفذ الاكشن

				if (excutedAction.Result is ObjectResult objectResult && objectResult.Value is not null)
				{
					var handleActionResult = new HandleActionResult
					{
						StatusCode = objectResult.StatusCode ?? StatusCodes.Status200OK,
						Content = objectResult.Value is string str ? str : JsonSerializer.Serialize(objectResult.Value, cacheRepo.jsonSerializerOptions)
					};
					await cacheRepo.setCacheInRedis(cacheKey, handleActionResult, _timeLifeInDays);
				}
			}
			else
			{
				context.Result = new ContentResult
				{
					Content = getCache.Content,
					StatusCode = getCache.StatusCode,
					ContentType = "application/json"
				};
			}
        }

		private string GetCacheKey(ActionExecutingContext context)
		{
			//"GET=>API/PRODUCT?BRANDID=4&CATEGORYID=4&PAGEINDEX=44&PAGESIZE=44&SEARCH=4&SORT=NAME"
			var request = context.HttpContext.Request;

			//Get Method
			var method = context.HttpContext.Request.Method;//=>Get
			//Get Path
			var path = context.HttpContext.Request.Path.Value?.Trim('/') ?? string.Empty; //=>api/product/
			//Get Query 
			var query = context.HttpContext.Request.Query; //IQueryCollection: IEnumerable<KeyValuePair<string, StringValues>>
			var ListQuery = query.Count > 0 ?
				query.OrderBy(KVPair => KVPair.Key)
				.Select( // مع كل كي فاليو بير رجعلي مكانها استرنج زي م عملت تحت كده ف هو كان داخل ليسته من كي فاليو بير هيرجع ليسته من الاسترنج
				KVPair =>
				{
					var Key = KVPair.Key;
					var value = KVPair.Value.Count > 0 ? KVPair.Value.Where(s => !string.IsNullOrWhiteSpace(s)).Select(s => Uri.EscapeDataString(s!)) : null;            //StringValues : IList<string?>
					return value != null ? $"{Key}={string.Join(',', value)}" : $"{Key}";
				}
				) : null;
			//"GET=>API/PRODUCT?BRANDID=4&CATEGORYID=4&PAGEINDEX=44&PAGESIZE=44&SEARCH=4&SORT=NAME"
			var returnedCacheKey = ListQuery != null ? $"{method}=>{path}?{string.Join('&', ListQuery)}" : $"{method}=>{path}";
			return returnedCacheKey.ToUpperInvariant();
		}
	}
}