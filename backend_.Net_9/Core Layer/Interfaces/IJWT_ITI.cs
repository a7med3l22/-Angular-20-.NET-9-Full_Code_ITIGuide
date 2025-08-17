using Core_Layer.Models.AngularITI_Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.Interfaces
{
	public interface IJWT_ITI
	{
		Task<string> MakeToken(RegisterUser registerUser);
	}
}
