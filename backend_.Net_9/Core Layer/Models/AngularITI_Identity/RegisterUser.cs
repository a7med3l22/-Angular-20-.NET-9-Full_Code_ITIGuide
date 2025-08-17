using Core_Layer.Models.Product;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace Core_Layer.Models.AngularITI_Identity
{
	public class RegisterUser : IdentityUser // ورثت اللي انا عاوزة من هنا 
	{
		// كل يوزر عنده اكتر من شيب ادريس و اخلي العلاقة وان تو ميني والادريس هيبقي ف جدول لواحده 
		public virtual ICollection<ShipAddress> ShipAddress { get; set; } = new HashSet<ShipAddress>(); // مش لازم علشان يسجل يحط shipAddress 

		// كل يوزر هيبقي عنده اكتر من فون يبقي اعمل الفون ف جدول لواحده واخليها وان تو ميني 
		// هتجاهلها لما اعمل الجدول هتجاهل ال PhoneNumber اللي ف ال identity وهخليه يعملها ignore وهو بينشأ الداتا بيز , هعمل new علشان اشيل الوورننج
		// عملت igonre ف ال config
		//[NotMapped]  // متعملهاش لو بتعمل اوفر رايد ف الكلاسات المشتقة يعني 
		//public override string? PhoneNumber { get; set; }
		//[NotMapped]
		//public override bool PhoneNumberConfirmed { get; set; }

		public virtual  ICollection<UserPhoneNumber> PhoneNumbers { get; set; } = new HashSet<UserPhoneNumber>(); // مش لازم علشان يسجل يحط shipAddress 
		// عاوز يبقي موجود ف الداتا بيز فيرت نيم و لاست نيم ف نفس الجدول وديسبلاي نيم يبقي محفوظ فيها الاتنين مع بعض  وقبلهم مستر ولا اقولك خليها ف جدول لوحدها احسن علشان مملاش الجدول ده 
		public virtual required Name Name { get; set; }

		/* (Gold)
				لو مجبتش البيانات باستخدام Eager Loading (يعني من غير .Include())، فممكن الخاصية المرتبطة تبقى null.
في الحالة دي، لازم أكتبها virtual علشان EF Core يقدر يعمل Lazy Loading ويحقن كود يحمل البيانات تلقائيًا لما أنادي على الخاصية.
أما لو جبت البيانات بـ .Include() من البداية، مش محتاج virtual لأن مش هيحصل Lazy Loading أصلاً.	
		

		public override string? PhoneNumber { get => base.PhoneNumber; set => base.PhoneNumber = value; }
		شبه اللي تحت 
		 public override NavigationPropertyType NavigationProperty
{
	get
	{
		if (base.NavigationProperty == null)
		{
			// الكود اللي يجيب القيمة من الداتا بيز
			base.NavigationProperty = LoadFromDatabase();
		}
		return base.NavigationProperty;
	}
	set
	{
		base.NavigationProperty = value;
	}
}

		 
		 
		 */

		//public int password { get; set; }  // مش هحفظه ف الداتا بيز
		//public required string email { get; set; } // موروث مش هعمله تاني 
		// هعمل الكونفيجريشنز بقي بين الجدول ده واللي فيه 


		/*
		{
	  "displayName": // موروث
	{
		"displayfirstName": "Ahmed",
		"displaylastName": "Alaa"
	  },
	  "address": {
		"firstName": "Ahmed",
		"lastName": "Alaa",
		"street": "Behind The Faculty Of Commerce",
		"city": "20",
		"country": "مصر"
	  },
	  "email": "ahmedaladdinmohamed@gmail.com", //موروث
	  "password": "2222",
	  "confirmPassword": "2222",
	  "phoneNumber": //// موروث  ولكن عاوزة ميبقاش واحد بس عاوزة يبقي انا اللي احدد كام واحد بناء ع اللي جايلي م الفرونت
		[
		"01558561998",
		"01279428817"
	  ]
	}
	 */		
	}
}
