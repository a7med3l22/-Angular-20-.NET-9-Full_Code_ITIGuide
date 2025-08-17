using Core_Layer.Models.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.GenericRepository.Data.Configurations
{
	public class orderItemConfigurations : IEntityTypeConfiguration<OrderItem>
	{
		public void Configure(EntityTypeBuilder<OrderItem> builder)
		{
			builder.Property(OI => OI.Price).HasPrecision(7, 2);
			builder.OwnsOne(OI => OI.productItemOrdered);
		}
	}
}
