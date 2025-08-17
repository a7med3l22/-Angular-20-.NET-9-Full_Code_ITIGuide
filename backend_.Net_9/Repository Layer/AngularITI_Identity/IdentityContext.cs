using Core_Layer.Models.AngularITI_Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repository_Layer.GenericRepository.Data.Configurations.AngularITI_IdentityConvigrations;
using Repository_Layer.IdentityRepository.Data.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.AngularITI_Identity
{
	//public class IdentityDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
	//public class IdentityContext : IdentityDbContext // لو عملت كده بس هيستخدم IdentityUser مش RegisterUser ف لازم اعمل زي اللي سطر اللي تحت 
	public class IdentityContext:IdentityDbContext<RegisterUser>
	{
		//عاوز ابعت ال options
		public IdentityContext(DbContextOptions options) :base(options) // كده بعت الاوبشن
		{
			
		}

		// عاوز اعرفه بقي ب ال config 
		protected override void OnModelCreating(ModelBuilder builder)
		{
			
			base.OnModelCreating(builder);

			//طالما هو اللي هيستخدم الفانكشن اللي بعتهاله ف البراميتر يبقي هو اللي هيبعتلها البراميتر الموضوع بيبقي سهل جدا لما بيتعرف !
			builder.Entity<RegisterUser>(x=>x.Ignore(u => u.PhoneNumber).Ignore(u=>u.PhoneNumberConfirmed)); // افضل حاجة اعملها من هنا علشان لما ينشأ الداتا بيز ميحطهومش حتي لو البروبيرتي موروثه من الاب 
			//دي او اللي تحتها عادي 
			//builder.Entity<RegisterUser>().Ignore(u => u.PhoneNumber).Ignore(u => u.PhoneNumberConfirmed);
			/*
					الـ static method بتتحمّل في الذاكرة أول مرة يتم استدعاؤها أو يتم تحميل الكلاس اللي فيها.

					بتفضل موجودة في الذاكرة طول ما التطبيق شغّال.

					بتكون shared بين كل الـ instances (لو فيه)، ومفيش منها نسخ مختلفة.
			 */
			//ApplyConfigurationsFromAssembly(Assembly assembly, Func<Type, bool>? predicate = null)


			Assembly assembly = Assembly.GetAssembly(typeof(AngularITI_IdentityConvigrations))!;
			builder.ApplyConfigurationsFromAssembly(assembly, type //التايب دي بتاع البراميتر كل مرة بتتغير علي حسب الكلاس اللي معمول فيه كونفيج ف النيم اسبيس 
				=> type.Namespace==typeof(AngularITI_IdentityConvigrations).Namespace);
		}
	}
	// بعد م عملت كونفيج هو كده هيربط الكلاسات ببعض ومش محتاج اعمل dbset ف اعمل مايجريت بقي 

		//1-هورث من ال كونتيكست الجاهز ادينتتي
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
		// عاوز اضيف بقي الحاجات اللي مش موروثه ب ايدي 
		// عاوز اهندل اكني واخد الحاجات دي م ال فرونت كده بالظبط
		// هعمل ال ادريس الاول 
		// كل يوزر عنده اكتر من شيب ادريس و اخلي العلاقة وان تو ميني والادريس هيبقي ف جدول لواحده 
		// كل يوزر هيبقي عنده اكتر من فون يبقي اعمل الفون ف جدول لواحده واخليها وان تو ميني 
		// عاوز يبقي موجود ف الداتا بيز فيرت نيم و لاست نيم ف نفس الجدول وديسبلاي نيم يبقي محفوظ فيها الاتنين مع بعض  وقبلهم مستر ولا اقولك خليها ف جدول لوحدها احسن علشان مملاش الجدول ده 
		// هظبط دلوقتي الداتا بيز بحيث يبقي فيها العلاقات اللي قولتها دي 



	}


