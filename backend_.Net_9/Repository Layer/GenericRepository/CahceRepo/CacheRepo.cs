using Core_Layer.Interfaces;
using Core_Layer.Models.ErrorsHandle;
using ReTypeAllByMe.Attributes;
using StackExchange.Redis;
using System.Text.Json;

namespace Repository_Layer.GenericRepository.CahceRepo
{
	public class CacheRepo:ICacheRepo
	{
		private readonly IDatabase _connection;

		//public JsonSerializerOptions jsonSerializerOptions { get; } = jsonSerializerOptionsImplemented;// الفرق بين دي واللي تحتها ان دي بتتعرف القيمة الابتداشية بتاعتها هنا او ف الكونستراكتور 
		public JsonSerializerOptions jsonSerializerOptions => jsonSerializerOptionsImplemented; // انما دي بتتعرف فقط هنا مش ف  الكنستراكتور كمان

		private static readonly JsonSerializerOptions jsonSerializerOptionsImplemented = new(){ PropertyNamingPolicy = JsonNamingPolicy.CamelCase };// static علان تتعرف مرة واحدة طول عمر البرنامج

		public CacheRepo(IConnectionMultiplexer connection)
		{
			_connection = connection.GetDatabase();
		}
		public  async Task setCacheInRedis(string CacheKey, HandleActionResult handleActionResult,int timeLifeInDays)
		{
			if (CacheKey == null)
				throw new ThrowException(400, "CacheKey Can`t Be Null");

			var serializedValue = JsonSerializer.Serialize(handleActionResult, jsonSerializerOptions);

			var isSaved= await _connection.StringSetAsync(CacheKey, serializedValue,TimeSpan.FromDays(timeLifeInDays));
			if(!isSaved)
				throw new ThrowException(500, "Error When Saving!!");

		}

		public async Task<HandleActionResult?> getCacheInRedis(string CacheKey)
		{
			if (CacheKey == null)
				throw new ThrowException(400, "CacheKey Can`t Be Null");

			var jsonValue = await _connection.StringGetAsync(CacheKey);
			if (jsonValue.IsNull)
				return null;

			var deserilizedValue= JsonSerializer.Deserialize<HandleActionResult>(jsonValue!,jsonSerializerOptions);
			return deserilizedValue;

		}

	}
}
