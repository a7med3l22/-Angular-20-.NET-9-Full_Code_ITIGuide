using Core_Layer.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.Interfaces
{
	public interface IJwtToken
	{
		Task<string> CreateTokenAsync(ApplicationUser user);
	}
}
