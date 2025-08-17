using Core_Layer.Models.Product;
using Core_Layer.Models.Product.AngularITIProducts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.GenericRepository.Data.Configurations.AngularITIProducts
{
	public class ProductsAngularConfig : IEntityTypeConfiguration<angularProducts>
	{
		public void Configure(EntityTypeBuilder<angularProducts> builder)
		{
			builder.Property(p=>p.price).HasPrecision(12, 2);
			builder.Property(p => p.quantity).HasMaxLength(100);
			builder.HasOne(p=>p.category).WithMany().OnDelete(DeleteBehavior.Cascade).IsRequired();//on delete category product will deleted//required to have forignKey=>category
		}
	}
}
