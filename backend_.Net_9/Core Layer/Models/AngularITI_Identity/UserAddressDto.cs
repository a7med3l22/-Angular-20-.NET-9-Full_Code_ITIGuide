using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.Models.AngularITI_Identity
{
	public class UserAddressDto
	{
		public string FirstName { get; set; } = null!;
		public  string LastName { get; set; } = null!;
		public  string Street { get; set; } = null!;
		public  string City { get; set; } = null!;
		public  string Country { get; set; } = null!;
	}
}
