using Core_Layer.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.IdentityRepository
{
	public class ApplicationIdentityDbContext:IdentityDbContext<ApplicationUser>
	{
		/*
				عند استخدام:
				public class ApplicationIdentityDbContext : IdentityDbContext<ApplicationUser>
				فإن EF Core + Identity يقومان بإنشاء جدول:
				AspNetUsers
				ويعتبر هذا الجدول يمثل كلاس ApplicationUser بالكامل.
		 */
		public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options):base(options)  // Allows Dependency Injection to provide DbContextOptions for configuring the DbContext (connection string, provider, etc.)
		{

		}
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder); // (IdentityDbContext) يحتوي على إعدادات جاهزة لتكوين جداول الهوية (Identity) //هذه الإعدادات تتم داخل OnModelCreating في IdentityDbContext.
			builder.ApplyConfigurationsFromAssembly(assembly: typeof(ApplicationIdentityDbContext).Assembly, type => type.Namespace == "Repository_Layer.IdentityRepository.Data.Configurations");

		}
		public DbSet<Address> Addresses { get; set; }
	}
}
