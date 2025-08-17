using Core_Layer.Models.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.GenericRepository.Data.Configurations
{
	public class OrderConfiguration : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder.Property(O => O.orderState).HasConversion(
				OS => OS.ToString(),//save to DB as string
				OS => Enum.Parse<OrderState>(OS) //returned from database as string , but convert it to enum 
				);

			builder.OwnsOne(o => o.shippingAddress
			//, OwnedBuilder =>
			//{
			//	OwnedBuilder.Property(sa => sa.FirstName).IsRequired();
			//	OwnedBuilder.Property(sa => sa.LastName).IsRequired();
			//	OwnedBuilder.Property(sa => sa.Street).IsRequired();
			//	OwnedBuilder.Property(sa => sa.City).IsRequired();
			//}
			);

			builder.HasOne(o=>o.deliveryMethod).WithMany().OnDelete(DeleteBehavior.SetNull).HasForeignKey(o=>o.DeliveryMethod_Id); //on delete principal (DeliveryMethod) the ForignKey[DeliveryMethodId] in dependent (Order) will be null
			// ده معناه ان الفورين كي في  ال اوردر لازم يبقي نلابول

			// الافضل اني اعمل العلاقة دي ناحية ال ديبيندينت لو انا هعمل كونفجريشن فيها ويكون طبعا في نافيجيشنل بروبرتي لل برينسيبل جوه ال انديبيندينت
			builder.HasMany(o=>o.orderItems).WithOne().OnDelete(DeleteBehavior.Cascade).IsRequired();//on delete principal (Order) the dependent (OrderItem) will be deleted//IsRequired() mean that forignkey in dependent(orderitems) cant set to null

			builder.Property(o => o.SubTotal).HasPrecision(10, 2);


		
		}
	}
}
