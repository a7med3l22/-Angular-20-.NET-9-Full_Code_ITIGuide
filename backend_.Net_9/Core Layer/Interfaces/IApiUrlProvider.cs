using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.Interfaces
{
	public interface IApiUrlProvider
	{
		string GetApiUrl(); // Method to get the base URL of the API, e.g., "http://localhost:5000/"
	}
}
