using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.Models.AngularITI_Identity
{
	public class UserLoginDto
	{
		// عاوز اعمل لوجين ب اليوزر نيم او ب الباسوورد 

		public string UserOrEmail { get; set; } = null!;
		public string Password { get; set; } = null!;


	}
}
