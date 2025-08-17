using Core_Layer.Interfaces;
using Core_Layer.Models.Product;
using Microsoft.EntityFrameworkCore;
using Repository_Layer.GenericRepository;
using Repository_Layer.GenericRepository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.UnitOfWork
{
	// لما اطلب جينيرك ريبوزيتري اوف تايب معين تدهولي ولو موجود نسخة منه تديني النسخة دي 
	public class UnitOfWork: IUnitOfWork,IDisposable
	{

		private readonly Dictionary<Type, object> keyValuePairs=new(); // كده عمرها مرتبط بعمر ال UnitOfWork , انما لو استخدمت generic هيبقي عمرها مرتبط بعمر التطبيق كله 
		private readonly ApplicationDbContext _dbContext;

		public UnitOfWork(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public IGenericRepository<T> GetRepo<T>() where T : BaseClass
		{
			Type type = typeof(T);

			if (!keyValuePairs.ContainsKey(type))
				keyValuePairs[type] = new GenericRepository<T>(_dbContext); //هنا حفظت ف الديكشينري نسخة واحدة من ال جينيرك ريبوزيتري وهترجعلي نفس النسخة طول عمر ال يونت اوف وورك والنسخة اللي هترجع دي هتبقي استيت ليس لانه لا يحتوي علي اي فيلد ميوتابول مثل ليست او ديكشينري او كاونتر داخلي يتغير وكل ال فيلدس اللي جواها قيمتها مش هتتغير  لان كل مرة انشأ فيها نسخة ال فيلد كونتيكست هيفضل نفس القيمة ومش هتتغير قيمته طول استخدام النسخة دي  , انما لو كان معايا فيلد عبارة عن ليست مثلا ف الليست دي ممكن اضيف فيها ف ب التالي مع كل نسخة ممكن قيمة الليست فيلد تتغير لاني ممكن اضيف جوه فيلد ليست دي ف ب التالي لما كنت عامل نسخة اول مرة الليست كانت فاضية بعد كده مبقتش فاضية ف لو جيت استخدمت نفس النسخة مش هلاقي الليست فاضية وهلاقيها ب اخر حالة ليها انما لو استخدمت نسخة جديدة هلاقي الليست فاضية
			return (IGenericRepository<T>)keyValuePairs[type];
		}
		public async Task<int> CompleteAsync()
		{
			return await _dbContext.SaveChangesAsync();
			
		}
//		public async Task AddProduct(int categoryId,int brandId)
//		{
//			var categoy = await _dbContext.Categories.FindAsync(categoryId);
//			//var categoy = new Category { Id=1, Name="Frappuccino" };
//			var brand1 = await _dbContext.Brands.FindAsync(1); // جيه من نفس الكونتيكست اذا هيعمل ريليشن شيب عادي 
//			var brand2 = await _dbContext.Brands.FindAsync(2); // جيه من نفس الكونتيكست اذا هيعمل ريليشن شيب عادي 
//			var brand3 = await _dbContext.Brands.FindAsync(1); // جيه من نفس الكونتيكست اذا هيعمل ريليشن شيب عادي 

//			/*
//			 لأنك جبت الكائن من الـ DbContext، وده معناه إن EF Core عارف إن الكائن ده موجود فعلاً في قاعدة البيانات، وبالتالي هيعمل ارتباط (relationship) مش Insert.
//			✅ Entity Framework (EF) بيقدر يفرق بين الكيان (object) المربوط بالـ DbContext، والكيان الجديد اللي انت عملته بإيدك ومش مربوط بأي tracking.

//			Entity Framework عارف إن الـ brand ده موجود أصلًا في الداتا بيز، لأنه fetched داخل نفس الـ DbContext.

//و لو جبته من كونتيكست تاني وجيت اضيفه ف ده هيتعامل معاه اكني بتعامل مع اوبجيكت انا عامله ب ايدي	 
//			 */
//			var product1 = new Product
//			{
//				//Category = categoy!,
//				//Brand= brand1!,
//				Description="teeeeeeest",
//				Name="Test",
//				PictureUrl="adadad",
//				Price=10,
//				BrandId=1,
//				CategoryId=2
//			};
//			var product2 = new Product
//			{
//				//Category = categoy!,
//				//Brand = brand2!,
//				Description = "teeeeeeest",
//				Name = "Test",
//				PictureUrl = "adadad",
//				Price = 10,
//				BrandId = 1,
//				CategoryId = 2
//			};
//			var product3 = new Product
//			{
//				//Category = categoy!,
//				//Brand = brand1!,
//				Description = "teeeeeeest",
//				Name = "Test",
//				PictureUrl = "adadad",
//				Price = 10,
//				BrandId = 1,
//				CategoryId = 2
//			}; 
//			var product4 = new Product
//			{
//				//Category = categoy!,
//				//Brand = brand3!,
//				Description = "teeeeeeest",
//				Name = "Test",
//				PictureUrl = "adadad",
//				Price = 10,
//				BrandId = 1,
//				CategoryId = 2
//			};
//			//مهم
//			// كده هيعمل عادي انما لو عملت await _dbContext.SaveChangesAsync(); ف الاخر خالص هيدي ايرور لانه لما يضيف البرودكت بيعمل تتبع للكاتيجوري والبراند وبعد م يضيف خلاص مش بيعمل تتبع علشان كده لو ضفتها للاخر هيدي ايرور لانه مش هيعرف يتتبع نفس الكاتيجوري او البراند 
//			_dbContext.Products.Add(product1);

//			_dbContext.Products.Add(product2);

//			_dbContext.Products.Add(product3);

//			_dbContext.Products.Add(product4);
//			await _dbContext.SaveChangesAsync();
//		}
		public void Dispose()
		{
			//اللي هقوله مهم جدا  

			// لو الخدمة مسجله ك اسكوب في ال DI
			// الخدمة دي بيتعمل نسخة منها لما تتطلب من ال DI والنسخة دي بتكون موجوده في ال DI CONTAINER طول عمر الاسكوب 
			// وعمر الاسكوب اللي بيتحكم فيه ال DI لو هو Per Request PipeLine
			// ولو بره ال ريكويست بايب لاين انا بنشأ اسكوب جديد عن طريق createScope
			// واي خدمة اطلبها من الاسكوب ده scope.ServiceProvider.GetRequiredService<MyScopedService>();  زي دي كده 
			// بتعمل نسخة منها وبيتم يتم التخلص من النسخة عند عمل ديسبوز لل اسكوب سواء ب استخدام  using(scope) هنا لما البلوك يخلص بيتم تنفيذ scope.dispose أو ب استخدام scope.dispose يدويا لو مش عاوز استخدم using 
			// وانتهاء البلوك بيتعرفه لو عملت {} ولو معملتش {} بيعرف نهاية البلوك لما يقابل اول ريتيرن
			// ولما اعمل  Using(scope) بيتم عمل  scope.dispose عند انتهاء البلوك ولما بيتعمل ديسبوز لل اسكوب اي خدمة ف الاسكوب  بتعمل امبليمنت ل IDisposable بينفذ ديسبوز ميثود بتاعتها, واي نسخة من الخدمة اتعملت ف الاسكوب ده بيحررها .
			// لو الخدمة مسجله ك ترانسيت في ال DI
			// بيتم انشاء نسخة منها عن طلب الخدمة من ال DI ومش بيتم الاحتفاظ ب النسخة دي 
			// لو الخدمة بتعمل امبليمنت ل IDisposable بيتم تنفيذ ديسبوز ميثود لو
			// استخدمت using لما استدعي الخدمة 
			// او لو حقنت الخدمة داخل خدمة اسكوب او خدمة سيبنجيلتون والخدمة الام تكون بتعمل امبليمنت ل IDisposable
			// وقتها لما يتم استدعاء ديسبوز ميثود للام بينفذ ديسبوز ميثود للخدمة الترانسيت اللي محقونه فيها 
			// غير كده مش هيتم استدعاء ديسبوز ميثود بتاعت الخدمة ترانسيت 

			// لو الخدمة مسجله ك سينجسلتون في ال DI
			// الخدمة دي بيتعمل نسخة منها لما تتطلب من ال DI والنسخة دي بتكون موجوده في ال DI CONTAINER طول عمر التطبيق 
			// ولما التطبيق ينتهي بيتم تحرير النسخة اللي اتعملت وبيتم تنفيذ ديسبوذ ميثود لو الخدمة بتعمل امبليمينت ل IDisposable

		}
	}
}




	//في C#، الأصناف (classes) الجنيريك لا تعتبر متوافقة مع بعضها حتى لو كان النوع بداخلها متوافق // لذلك: لا يسمح لك بعمل Casting صريح بينهما أثناء الـ Compile Time.
