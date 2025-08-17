using Core_Layer.Interfaces;
using Core_Layer.Models.Product;
using Core_Layer.Specification;
using Microsoft.EntityFrameworkCore;
using Repository_Layer.GenericRepository;
using Repository_Layer.GenericRepository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.GenericRepository
{
	//use It To GetAllAsync, GetAsyncById in DataBase
	public class GenericRepository<T>:IGenericRepository<T> where T : BaseClass
	{
		private readonly ApplicationDbContext context;
		public GenericRepository(ApplicationDbContext context)
		{
			this.context = context;
		}
		public async Task<IReadOnlyList<T>> GetAllAsync() //نعم، IReadOnlyList تقوم بكل ما يقوم به IEnumerable وتزيد عليه بالـ Indexing والـ Count.
		{
			return await context.Set<T>().AsNoTracking().ToListAsync();
		}
		public async Task<T?> GetAsyncById(int id)
		{
			return await context.Set<T>().FindAsync(id);
		}
		public async Task<IReadOnlyList<T>> GetAllAsyncAfterSpec(ISpecifications<T> spec)//IReadOnlyList means that the list is read-only and cannot be [(Add,Remove) Elements In List ], But You Can [Modified Elements In List].
		{
			var query = ApplySpecification(spec);
			return await query.AsNoTracking().ToListAsync(); // AsNoTracking() is used to improve performance when you don't need to track changes to the entities.
															 // If you make any edits to the returned entities, these changes will not be tracked or saved to the database.
		}
		public async Task<T?> GetAsyncAfterSpecId(ISpecifications<T> spec)
		{
			var query = ApplySpecification(spec);
			return await query.FirstOrDefaultAsync();
		}
		public async Task<int> GetCountAsync(ISpecifications<T> spec)
		{
			return await ApplySpecification(spec).CountAsync();
		}
		private IQueryable<T> ApplySpecification(ISpecifications<T> spec) //✅ يتم تنفيذ الفلترة والتحديد في قاعدة البيانات، وليس في الذاكرة.
		{
			return context.Set<T>().GetQuery(spec);
		}

		public void Add(T item)
		{
			context.Add(item);
		}
		public void RemoveRange(IEnumerable<T> values)
		{
			context.RemoveRange(values);
		}
		public void Update(T entity)
		{
			context.Update(entity);
		}
	}
}
/*
 IReadOnlyList<T> تعني القائمة نفسها ثابتة (Read-Only)، ولكن محتوى العناصر داخلها يمكن تعديله إذا كانت العناصر Reference Types.

IReadOnlyList<Product> ✔
IReadOnlyList<int> ×

 */


/*   ملحوظة عن عمر الفيلد ده <3
 
keyValuePairs[type] = new GenericRepository<T>(_dbContext);
هيتم تحرير ال 		private readonly ApplicationDbContext context;
اللي داخل new GenericRepository<T>(_dbContext);
لما ال keyValuePairs[type]  ينتهي ب انتهاء ال unitOfWork 

 */

/*
تخيل الكود التالي داخل GenericRepository:

private readonly List<T> _entitiesCache = new();

public async Task<List<T>> GetCachedEntitiesAsync()
{
    if (_entitiesCache.Count == 0) // لو الكاش فاضي
    {
        var data = await context.Set<T>().AsNoTracking().ToListAsync(); // يتم تحميل البيانات من الداتا بيز
        _entitiesCache.AddRange(data); // يتم حفظ البيانات في الكاش
    }
    return _entitiesCache; // إرجاع البيانات من الكاش
}

لو استدعيت المستودع مرتين داخل نفس السكوب:
var repo1 = unitOfWork.GetRepo<Product>();
var products1 = await repo1.GetCachedEntitiesAsync();

var repo2 = unitOfWork.GetRepo<Product>();
var products2 = await repo2.GetCachedEntitiesAsync();

لو **لا تستخدم Dictionary:
🔹 كل مرة تنفذ GetRepo<Product>() ➔ تحصل على نسخة جديدة من GenericRepository<Product>.
🔹 عند استدعاء GetCachedEntitiesAsync() على كل نسخة:
 سيتم تنفيذ:
var data = await context.Set<T>().AsNoTracking().ToListAsync();
 مرتين 
(أي يتم جلب البيانات من الداتا بيز مرتين).

🔹 كل نسخة لديها _entitiesCache خاص بها، لذلك كل نسخة لا تعلم عن الكاش المحمّل في الأخرى.


 لو تستخدم Dictionary<Type, object>:
🔹 عند استدعاء GetRepo<Product>() لأول مرة ➔ تنشئ نسخة واحدة وتخزنها في الـ Dictionary.
🔹 عند استدعاء GetRepo<Product>() مرة أخرى ➔ تُرجع نفس النسخة من المستودع.
🔹 عند استدعاء GetCachedEntitiesAsync() لأول مرة:

يتم جلب البيانات من قاعدة البيانات.

يتم تخزين البيانات في _entitiesCache.
🔹 عند استدعاء GetCachedEntitiesAsync() مرة أخرى:

لن يتم تحميل البيانات من قاعدة البيانات مرة أخرى.

سيتم إرجاع نفس البيانات الموجودة بالفعل في _entitiesCache.
 */
//هنا حفظت ف الديكشينري نسخة واحدة من ال جينيرك ريبوزيتري وهترجعلي نفس النسخة طول عمر ال يونت اوف وورك والنسخة اللي هترجع دي هتبقي استيت ليس لانه لا يحتوي علي اي فيلد ميوتابول مثل ليست او ديكشينري او كاونتر داخلي يتغير وكل ال فيلدس اللي جواها قيمتها مش هتتغير  لان كل مرة انشأ فيها نسخة ال فيلد كونتيكست هيفضل نفس القيمة ومش هتتغير قيمته طول استخدام النسخة دي  , انما لو كان معايا فيلد عبارة عن ليست مثلا ف الليست دي ممكن اضيف فيها ف ب التالي مع كل نسخة ممكن قيمة الليست فيلد تتغير لاني ممكن اضيف جوه فيلد ليست دي ف ب التالي لما كنت عامل نسخة اول مرة الليست كانت فاضية بعد كده مبقتش فاضية ف لو جيت استخدمت نفس النسخة مش هلاقي الليست فاضية وهلاقيها ب اخر حالة ليها انما لو استخدمت نسخة جديدة هلاقي الليست فاضية