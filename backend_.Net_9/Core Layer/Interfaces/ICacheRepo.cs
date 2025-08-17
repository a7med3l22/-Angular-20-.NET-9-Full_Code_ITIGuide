using ReTypeAllByMe.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core_Layer.Interfaces
{
	public interface ICacheRepo
	{
		Task setCacheInRedis(string CacheKey, HandleActionResult handleActionResult, int timeLifeInDays);
		Task<HandleActionResult?> getCacheInRedis(string CacheKey);
		JsonSerializerOptions jsonSerializerOptions { get; }
	}
}
