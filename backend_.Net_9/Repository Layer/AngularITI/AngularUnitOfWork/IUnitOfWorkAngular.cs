using Core_Layer.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.AngularITI.AngularUnitOfWork
{
	public interface IUnitOfWorkAngular
	{
		GenericRepositoryAngular<T> GetRepo<T>() where T : BaseClass;
		Task<int> SaveChanges();
	}
}
