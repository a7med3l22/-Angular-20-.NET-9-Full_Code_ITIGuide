using Core_Layer.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.Interfaces
{
	public interface IUnitOfWork
	{
		IGenericRepository<T> GetRepo<T>() where T : BaseClass;
		Task<int> CompleteAsync();
		//Task AddProduct(int categoryId, int brandId);
	}
}
