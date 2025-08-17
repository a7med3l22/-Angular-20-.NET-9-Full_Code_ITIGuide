using Core_Layer.Models.AngularITI_Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.GenericRepository.Data.Configurations.AngularITI_IdentityConvigrations
{
	public class AngularITI_IdentityConvigrations : IEntityTypeConfiguration<RegisterUser>
	{
		//هام جدا لو فيه اي بروبيرتي موجوده لازم احددها لازم علشان ال EF بيتلغبط
		public void Configure(EntityTypeBuilder<RegisterUser> builder)
		{
			//one user has many shipAddress .. shipAddress Will Has ForignKey--UserId--
			builder.HasMany(u=>u.ShipAddress).WithOne().IsRequired()//  الافضل طبعا اني اعمل دي من ناحيه ال التابع يعني اللي فيه ال فورين كي بس لو مش هعمل كونفيجر ناحيتها ف معملش مخصوص اعمل هنا وخلاص
			.OnDelete(DeleteBehavior.Cascade);// يعني لو مسحت اليوزر الادريس يتمسح 
			// زي دي طالما موجود ال PhoneNumbers احددها زي كده 
			builder.HasMany<UserPhoneNumber>(u => u.PhoneNumbers).WithOne().IsRequired()// يعني لازم ال تابع يبقي ال فورين كي بتاعته مش بنال IsRequired
			.OnDelete(DeleteBehavior.Cascade);// يعني لو مسحت اليوزر الادريس يتمسح  , لو مسحت المتبوع التابع يتمسح يعني 
			builder.HasOne(u=>u.Name).WithOne().HasForeignKey<Name>().IsRequired(); // هخلي ال نيم التابع  // HasForeignKey<Name>() لأازم اعملها جينيرك علشان يعرف ان ده التابع لان العلاقة وان تو وان ف لازم احدد التابع اللي عنده ال فورين كي 
			//builder.Ignore(u => u.PhoneNumber).Ignore(u=>u.PhoneNumberConfirmed); // علشان ميتحفظوش ف الداتا بيز
//The exception 'The property 'PhoneNumber' cannot be ignored on type 'User' because it's declared on the base type 'IdentityUser'.
	//To exclude this property from your model, use the [NotMapped] attribute or 'Ignore' on the base type in 'OnModelCreating'.
	}
	}
}
