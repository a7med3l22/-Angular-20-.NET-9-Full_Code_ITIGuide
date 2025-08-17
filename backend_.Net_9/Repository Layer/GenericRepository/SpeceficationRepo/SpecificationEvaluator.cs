using Core_Layer.Models.Product;
using Core_Layer.Specification;
using Microsoft.EntityFrameworkCore;

namespace Core_Layer.Specification
{
	public static class SpecificationEvaluator
	{
		//مهم جدااا
		// GetQuery<T>(this IQueryable<T> query, ISpecifications<T> spec)
		//this IQueryable<T> query 
		// معناها ان الميثود جيت كويري دي اكنها بقيت جوه ال اكويرابول يعني اي حاجة اي كيورابول لو عملت بعدها دوت هيطلعلي الميثود دي 
		//query in this IQueryable<T> query 
		// الكويري دي بتساوي الحاجة ال اي كيورابول اللي انا عملت بعدها دوت وجبت الميثود دي
		//هستخدمها كده 
		//return context.Set<T>().GetQuery(spec);
		public static IQueryable<T> GetQuery<T>(this IQueryable<T> query, ISpecifications<T> spec) where T : BaseClass//query=IQueryable<T>
		{
			if (spec.Criteria is not null)
				query = query.Where(spec.Criteria); // dbContext.Set<T>().Where()
			//لو spec.Includes فاضي، لن يدخل Aggregate في أي Loop وسيعيد query كما هو.
			query = spec.Includes.Aggregate(query,(query,include)=>query.Include(include)); // dbContext.Set<T>().Where().include(p => p.Brand).Include(p => p.Category)

		

			if (spec.OrderBy is not null)
				query = query.OrderBy(spec.OrderBy); // dbContext.Set<T>().Where().include(p => p.Brand).Include(p => p.Category).Skip(0).Take(6).OrderBy(p => p.Id)
			if (spec.OrderByDesc is not null)
				query = query.OrderByDescending(spec.OrderByDesc); // dbContext.Set<T>().Where().include(p => p.Brand).Include(p => p.Category).Skip(0).Take(6).OrderByDescending(p => p.Id)


			if (spec.IsPagingEnabled)
			{
				query = query.Skip(spec.Skip).Take(spec.Take); // dbContext.Set<T>().Where().include(p => p.Brand).Include(p => p.Category).Skip(0).Take(6)
			}

			return query;
		}
	}
}
