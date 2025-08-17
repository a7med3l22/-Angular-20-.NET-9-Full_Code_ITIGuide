using Core_Layer.Models.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.GenericRepository.Data.Configurations
{
	public class ProductConfiguration : IEntityTypeConfiguration<Product>
	{		
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
			builder.Property(p => p.Description).IsRequired();
			builder.Property(p => p.Price).HasPrecision(8,2);
			builder.Property(p => p.PictureUrl).IsRequired();
			builder.HasKey(p => p.Id);
			//builder.HasOne(p=>p.Category).WithMany().HasForeignKey(p=>p.CategoryId).OnDelete(DeleteBehavior.Cascade);
			builder.HasOne(p=>p.Category).WithMany().OnDelete(DeleteBehavior.Cascade);
			//builder.HasOne(p =>p.Brand).WithMany().HasForeignKey(p => p.BrandId).OnDelete(DeleteBehavior.Restrict);
			builder.HasOne(p =>p.Brand).WithMany().OnDelete(DeleteBehavior.Restrict);

			//product has one category with many Product // Take Primary Key Of One As Foreign Key In Many
			//so product is Dependent on category 
			// If Principal is deleted, the Dependent  will also be deleted
			//So:- if category is deleted, the product will also be deleted because of the cascade delete behavior
			/*
						.OnDelete(DeleteBehavior.Restrict);
						هل يفرق لو وضعتها في Config الـ Principal أو Dependent؟
						من ناحية النتيجة النهائية: لا يفرق
						لأن:

						OnDelete تحدد الـ DeleteBehavior للعلاقة نفسها بغض النظر عن مكان كتابتها.

						EF Core سينفذ نفس الـ DeleteBehavior سواء حددته في جانب الـ Principal أو Dependent.

						2️⃣ من ناحية وضوح الكود: يفضل وضعها في الـ Dependent
						لأن العلاقة متعلقة بكيفية تعامل الـ Dependent مع حذف الـ Principal.

 
			 */
		}
	}
}
