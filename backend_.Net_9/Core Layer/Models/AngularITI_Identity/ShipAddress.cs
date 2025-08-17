using Core_Layer.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.Models.AngularITI_Identity
{
	public  class ShipAddress:BaseClass // علشان عاوزها تبقي ف جدول لوحدها ف الداتا بيز
	{
		/*
			"firstName": "Ahmed",
            "lastName": "Alaa",
            "street": "Behind The Faculty Of Commerce",
            "city": "20",
            "country": "مصر"
		 */
		public required string FirstName { get; set; } //required لو عملت انستانس منها لازم اديها فاليو 
		public required string LastName { get; set; } 
		public required string Street { get; set; } 
		public required string City { get; set; } 
		public required string Country { get; set; }
		//public required string userId { get; set; }
	}
}
