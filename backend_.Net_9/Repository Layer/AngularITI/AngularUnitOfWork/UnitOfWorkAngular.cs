using Core_Layer.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.AngularITI.AngularUnitOfWork
{
	public class UnitOfWorkAngular:IUnitOfWorkAngular,IDisposable
	{
		private readonly AngularDBContext angularDBContext;

		private Dictionary<Type, object> Repositories=new();
		public UnitOfWorkAngular(AngularDBContext angularDBContext)
		{
			this.angularDBContext = angularDBContext;
		}

		public GenericRepositoryAngular<T> GetRepo<T>() where T : BaseClass
		{
			var type=typeof(T);
			if (!Repositories.ContainsKey(type))
			{
				Repositories[type] = new GenericRepositoryAngular<T>(angularDBContext);
			}
			return (GenericRepositoryAngular<T>)Repositories[type];
		}
		public async Task<int> SaveChanges()
		{
			return await angularDBContext.SaveChangesAsync();
		}
		public void Dispose()
		{
			// بما إن AngularDBContext جاي من الـ DI والـ DI Container هيتكفّل بعملية الـ Dispose تلقائيًا،
			// فمش ضروري أستدعي Dispose هنا يدويًا إلا لو كنت بستخدم DbContext بشكل غير محقون (manual instantiation).
			// لكن مش غلط برضه إضافته احتياطيًا لو حصل تغيير مستقبلي في طريقة الاستخدام.
			angularDBContext.Dispose();
		}
	}
}
