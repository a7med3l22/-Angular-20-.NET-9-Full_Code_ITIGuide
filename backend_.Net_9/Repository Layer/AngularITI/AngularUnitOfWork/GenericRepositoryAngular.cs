using Core_Layer.Models.Product;
using Core_Layer.Specefication;
using Core_Layer.Specification;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.AngularITI.AngularUnitOfWork
{
	public class GenericRepositoryAngular<T> where T : BaseClass
	{
		private readonly AngularDBContext angularDBContext;

		public GenericRepositoryAngular(AngularDBContext angularDBContext) 
		{
			this.angularDBContext = angularDBContext;
		}
		public async Task<IReadOnlyList<T>?> GetAllAsync()
		{
			var items =await angularDBContext.Set<T>().AsNoTracking().ToListAsync();
			return items;
		}
		public async Task<IReadOnlyList<T>?> GetAllSpecAsync(ISpecifications<T> specifications)
		{
			//Get All With Includes			
			var items = await angularDBContext.Set<T>()//IQueryable<T>
				.GetQuery(specifications)//IQueryable<T>
				.AsNoTracking()//IQueryable<T>
				.ToListAsync();//Task<List<T>>
			return items;
		}
		public async Task<T?> GetByIdAsync(int id)
		{
			var itemById = await angularDBContext.Set<T>().FindAsync(id);
			return itemById;
		}
		public async Task<T?> GetByIdSpecAsync(int id,ISpecifications<T> specifications)
		{
			var itemById = await angularDBContext.Set<T>().GetQuery(specifications).FirstOrDefaultAsync(p=>p.Id==id);
			return itemById;
		}
		public void removeItem(T Item)
		{
			 angularDBContext.Set<T>().Remove(Item);
			
		}
		public async Task AddItemAsync(T Item)
		{
			 await angularDBContext.Set<T>().AddAsync(Item);
			
		}
		public void UpdateItem(T Item)
		{
			 angularDBContext.Set<T>().Update(Item);
		}
	}
}
