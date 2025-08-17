using Core_Layer.Models.Order;
using Core_Layer.Models.Product;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.GenericRepository.Data
{
	public class ApplicationDbContext:DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)//Dependency Injection Constructor
			: base(options) //options I Made In Program.cs Pass It To Base
		{
			

		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(assembly: Assembly.GetExecutingAssembly(), type => type.Namespace == "Repository_Layer.GenericRepository.Data.Configurations");

		}
		public DbSet<Product> Products { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Brand> Brands { get; set; }
		public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }

	}
}
/*
			
			علشان يحصل جدول لل برودكتس  مثلا في الداتا بيز

	             لازم اعمل دي
		public DbSet<Product> Products { get; set; }  
	                أو دي
	public class ProductConfiguration : IEntityTypeConfiguration<Product> 
                   اعمل كونفجريشن ليها يعني  

وطبعا اعمل ApplyConfigurations ف OnModelCreating

من خلال دول هيعرف ان فيه جدول اسمه برودكت ف الداتا بيز 
 
 */