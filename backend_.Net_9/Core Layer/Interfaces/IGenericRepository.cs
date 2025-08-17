using Core_Layer.Models.Product;
using Core_Layer.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.Interfaces
{
	public interface IGenericRepository<T> where T : BaseClass
	{
		 Task<IReadOnlyList<T>> GetAllAsync();

		 Task<T?> GetAsyncById(int id);

		 Task<IReadOnlyList<T>> GetAllAsyncAfterSpec(ISpecifications<T> spec);

		Task<int> GetCountAsync(ISpecifications<T> spec);
		Task<T?> GetAsyncAfterSpecId(ISpecifications<T> spec);
		public void Add(T item);
		public void Update(T item);

		public void RemoveRange(IEnumerable<T> values);
	


	}
}
