using Core_Layer.Models.Product;
using Core_Layer.Models.Product.AngularITIProducts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.AngularITI
{
	public class AngularDBContext:DbContext
	{

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			Assembly assembly = Assembly.GetExecutingAssembly();
			modelBuilder.ApplyConfigurationsFromAssembly(assembly,type=>type.Namespace== "Repository_Layer.GenericRepository.Data.Configurations.AngularITIProducts");
		}
	
		public AngularDBContext(DbContextOptions<AngularDBContext> options):base(options) 
		{


		}
		public DbSet<angularProducts> angularProducts { get; set; }
		public DbSet<Category> Categories { get; set; }

	}
}
