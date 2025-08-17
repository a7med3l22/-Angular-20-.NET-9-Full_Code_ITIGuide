using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.Models.Product.AngularITIProducts
{
	public class SetProductAngular
	{
		// عملتهم نلابول علشان لما اعمل ابديت ابعت اللي انا عاوز اعمله ابديا بس 
		public required string name { get; set; }  // ديفولت ب نال وخليتها تبقي نلابول علشان لو معملتش كده مش هيسمحلي اني ابعت حاجة  ب نال ف الكنترولر هيقولك انه ريكويرد
		public int categoryId { get; set; } // ديفولت ب صفر / انما لو مبعتش حاجة هنا هيبقي ب صفر مش هيقولي انه ريكويرد
		public decimal price { get; set; }
		public int quantity { get; set; }
		//public required string test { get; set; };

		public required IFormFile  pictureUrl { get; set; }

	/*
		public  string test { get; set; }; // لما اعمل اوبجيكت من الكلاس ده ممكن تبقي نال عادي بس لما يتعمل بايند ع الكلاس ده من الكنترولر هيعمله ف السواجر او البوست مان انها ريكويرد
		public required string test { get; set; }; //ريكويرت لما اعمل اوبجيكت من الكلاس ده وريكويرد لما يتعمل بايندج ع الكلاس ده 
		public  string? test { get; set; }; // مش ريكواير لما اعمل اوبجيكت من الكلاس ده ومش ريكويرد لما اعمل بايندج ع الكلاس ده 

	*/
	}
}
