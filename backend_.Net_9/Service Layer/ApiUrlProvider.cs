using Core_Layer.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Layer
{
	public class ApiUrlProvider: IApiUrlProvider
	{
		private readonly IHttpContextAccessor _httpContextAccessor; // This will allow us to access the current HTTP context

		public ApiUrlProvider(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}
		public string GetApiUrl()
		{
			var request = _httpContextAccessor.HttpContext?.Request;
			return $"{request?.Scheme}://{request?.Host.Value}/"; // Returns the base URL of the API, e.g., "http://localhost:5000/"

		}
	}
}
