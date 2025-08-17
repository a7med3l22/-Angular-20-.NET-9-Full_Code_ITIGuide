using Core_Layer.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.Models.AngularITI_Identity
{
	public class RegisterDtoITI
	{
		public  DisplayNameDto DisplayName { get; set; } = null!;
		public UserAddressDto Address { get; set; } = null!;
		public ICollection<string> PhoneNumber { get; set; } = null!;
		[EmailAddress]
		public  string Email { get; set; } = null!;
		public  string Password { get; set; } = null!;
		[Compare("Password")]
		public  string ConfirmPassword { get; set; } = null!;



		//	  {
		//         "displayName": {
		//           "displayfirstName": "Ahmed",
		//           "displaylastName": "Alaa"
		//         },
		//         "address": {
		//           "firstName": "Ahmed",
		//           "lastName": "Alaa",
		//           "street": "Behind The Faculty Of Commerce",
		//           "city": "20",
		//           "country": "مصر"
		//         },
		//         "email": "ahmedaladdinmohamed@gmail.com",
		//         "password": "2222",
		//         "confirmPassword": "2222",
		//         "phoneNumber": [
		//		"01558561998",
		//		"01279428817"
		//	  ]
		//}
	}
}
