using Core_Layer.Models.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.IdentityRepository.Data.Configurations
{
	public class AddressConfigurations : IEntityTypeConfiguration<Address>
	{
		public void Configure(EntityTypeBuilder<Address> builder)
		{
			builder.HasOne(Address=> Address.ApplicationUser)
				   .WithOne(ApplicationUser => ApplicationUser.Address)
				   .HasForeignKey<Address>(Address => Address.ApplicationUserId) // <Address> بنحدد هنا الفورين كي فانه ف ال Address 
				   .OnDelete(DeleteBehavior.Cascade);  // If Principal is deleted, the Dependent  will also be deleted //So-: If ApplicationUser is deleted, the Address will also be deleted 
		}
	}
}
