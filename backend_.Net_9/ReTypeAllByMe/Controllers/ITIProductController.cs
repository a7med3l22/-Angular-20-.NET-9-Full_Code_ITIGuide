using AutoMapper;
using Core_Layer.Models.ErrorsHandle;
using Core_Layer.Models.Product;
using Core_Layer.Models.Product.AngularITIProducts;
using Core_Layer.Specefication;
using Core_Layer.Specification;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer.AngularITI.AngularUnitOfWork;
using static System.Net.Mime.MediaTypeNames;
using File = System.IO.File;
using Directory = System.IO.Directory;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace ReTypeAllByMe.Controllers
{
	[Authorize]
	public class ITIProductController : BaseApiController
	{
		private readonly IUnitOfWorkAngular unitOfWork;
		private readonly IMapper mapper;

		public ITIProductController(IUnitOfWorkAngular unitOfWork, IMapper mapper)
		{
			this.unitOfWork = unitOfWork;
			this.mapper = mapper;
		}
		[HttpGet]
		public async Task<ActionResult<IReadOnlyList<angularProductsDto>>> GetITIProducts([FromQuery] FilterAndSortProduct filterAndSort)
		{
			//x(ssx);
			//x((x, y) => new AngularProductITISpec());
			//x((AngularProductITISpec x, AngularProductITISpec y) => new AngularProductITISpec());
			//x((_,_) => new AngularProductITISpec());
			//x((x, y) => { return new AngularProductITISpec(); });
			//x((x, y) => { return y; });

			//return Ok();
			//sort:name,price,quentity//filter:categoryId,name,category
			var spec = new AngularProductITISpec(filterAndSort);
			var products = await unitOfWork.GetRepo<angularProducts>().GetAllSpecAsync(spec);
			var angularProductsDto = mapper.Map<IReadOnlyList<angularProductsDto>>(products);
			return Ok(angularProductsDto);
		}
		//private AngularProductITISpec ssx(AngularProductITISpec x, AngularProductITISpec y)
		//{
		//	return new AngularProductITISpec();
		//}
		//private void x(Func<AngularProductITISpec, AngularProductITISpec, AngularProductITISpec> func)
		//{
		//	// اما تستقبل الفانكشن 
		//	AngularProductITISpec angularProductITISpec = new();
		//	AngularProductITISpec angularProductITISpecc = new() { Criteria = x => true };
		//	//var x = func.Invoke(new AngularProductITISpec(), angularProductITISpecc);
		//	var x = func.Invoke(angularProductITISpec, angularProductITISpecc);
		//	Console.WriteLine(angularProductITISpec.Includes[0]);
		//	Console.WriteLine(angularProductITISpecc.Includes[0]);
		//	Console.WriteLine(x.Includes[0]);
		//}
		//[HttpGet("test")]
		//public void GetITIProduct()
		//{
		//	//20
		//	//20
		//	//800
		//	//angularPro=> price 400 ,categoryId = 40,quantity = 6, Default
		//	// عاش
		//	var angularPro = new angularProducts() { categoryId = 40 };
			
		//	Console.WriteLine(x((x,y) => {

		//		x.categoryId = 20;
		//		y.categoryId = 20;
		//		return angularPro;
			
		//	}));//800
		//	Console.WriteLine(angularPro);
			
		//}
		//private int x(Func<angularProducts, angularProducts, angularProducts> xx)
		//{
		//	// عبارة عن فانكشن بترجع انجولر برودكت ومش بتاخد براميتر Func<angularProducts>
		//	var angularPro1 = new angularProducts() { categoryId = 200 };
		//	var angularPro2 = new angularProducts() { categoryId = 260 };
		//	var y =xx.Invoke(angularPro1, angularPro2);
		//	Console.WriteLine(angularPro1.categoryId); //20
		//	Console.WriteLine(angularPro2.categoryId);	//20
		//	y.price = 400;
		//	y.quantity = 6;
		//	return y.categoryId * 20;

		//}
		[HttpGet("{id}")]
		public async Task<ActionResult<IReadOnlyList<angularProductsDto>>> GetITIProduct(int id)
		{
			var spec = new AngularProductITISpec();
			var product = await unitOfWork.GetRepo<angularProducts>().GetByIdSpecAsync(id, spec);

			var angularProductDto = mapper.Map<angularProductsDto>(product);

			return Ok(angularProductDto);
		}
		[HttpPost]
		public async Task<ActionResult<angularProductsDto>> SetITIProduct(SetProductAngular setProductAngular)
		{
			if (!await saveImageToPc(setProductAngular.pictureUrl))
				return BadRequest(new HandleErrors(400, "An Error With Image!!"));

			var product = mapper.Map<angularProducts>(setProductAngular);
			await unitOfWork.GetRepo<angularProducts>().AddItemAsync(product);
			var result = await unitOfWork.SaveChanges();
			var spec = new AngularProductITISpec();
			var AddedProduct = await unitOfWork.GetRepo<angularProducts>().GetByIdSpecAsync(product.Id, spec);
			var returnedProduct = mapper.Map<angularProductsDto>(AddedProduct);
			return result > 0 ? Ok(returnedProduct) : BadRequest(new HandleErrors(400, "Not Added"));
		}
		private async Task<bool> saveImageToPc(IFormFile image)
		{
			if (image == null || image.Length == 0)
				return false;
			try
			{
				var foulderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imagesITI", "products");
				if (!Directory.Exists(foulderPath)) // هل المجلد موجود ؟
					Directory.CreateDirectory(foulderPath);

				var fileName = Path.GetFileName(image.FileName); // أمان ضد اسم الملف الخبيث//image.FileName=>Path.GetFileName(image.FileName) <><> "C:\\Users\\ahmed\\Downloads\\x.png"  =>	"x.png"
				var filePath = Path.Combine(foulderPath, fileName);
				//if (System.IO.File.Exists(filePath)) // هل الملف موجود ؟
				//	return;
				//// 🧼 اختياري: امسح الملف لو موجود مسبقًا
				//if (System.IO.File.Exists(filePath))
				//	System.IO.File.Delete(filePath);
				using (var stream = new FileStream(filePath, FileMode.Create)) //بتحفظه  ف المجلد بتاعي// FileMode.Create هيمسح أي ملف بنفس الاسم ويبدأ من جديد
					await image.CopyToAsync(stream);
				return true;
			}
			catch
			{
				// 🛡️ في حالة أي Exception (زي صلاحيات أو مشاكل في الديسك)
				return false;
			}
		}



		[HttpDelete("{id}")]
		public async Task<ActionResult<angularProductsDto>> DeleteITIProduct(int id)
		{
			var spec = new AngularProductITISpec();
			var product = await unitOfWork.GetRepo<angularProducts>().GetByIdSpecAsync(id, spec);
			if (product == null) { return BadRequest(new HandleErrors(400, "Not Found ProductWith This Id")); }
			unitOfWork.GetRepo<angularProducts>().removeItem(product);
			var result = await unitOfWork.SaveChanges();


			if (result > 0)
			{
				DeleteImageFromPc(product.pictureUrl);
				var returnedProduct = mapper.Map<angularProductsDto>(product);
				return Ok(returnedProduct);
			}
			else
			{
				return BadRequest(new HandleErrors(400, "Not Deleted"));
			}
		}

		private void DeleteImageFromPc(string? imageURL)
		{
			if (string.IsNullOrWhiteSpace(imageURL))
				return;

			try
			{
				var foulderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
				var fileName = imageURL.Replace('/', Path.DirectorySeparatorChar); // مهم جدا علشان يظبط ال اسلاشات اللي جايه 

				var filePath = Path.Combine(foulderPath, fileName);

				if (System.IO.File.Exists(filePath))
				{
					//System.IO.File.SetAttributes(filePath, FileAttributes.Normal); // تأكد إنه قابل للحذف
					System.IO.File.Delete(filePath);
				}
			}
			catch (Exception ex)
			{
				// هنا ممكن تسجل اللوج لو حابب تتبع المشاكل
				Console.WriteLine($"❌ Failed to delete image: {ex.Message}");
			}
		}




		/// اعمل update 
		[HttpPut("{id}")] // برافو
		public async Task<ActionResult<angularProductsDto>> UpdateITIProducts(int id, UpdateProductAngular UpdateProductAngular)
		{


			//var x = new {id= id,name= UpdateProductAngular };
			//Console.WriteLine(x.id);

			//var y = new {  id,  UpdateProductAngular };
			//Console.WriteLine(y.UpdateProductAngular);
			// عاوزة يحدث القيمة اللي بعتها بس واللي مبعتهاش تفضل زي م هي 
			var spec = new AngularProductITISpec();

			var getedProduct = await unitOfWork.GetRepo<angularProducts>().GetByIdSpecAsync(id, spec);
			if (getedProduct == null) { return BadRequest(new HandleErrors(400, "notFound Products With This Id")); }

			//if (UpdateProductAngular.pictureUrl == null)// عاوزة لو مبعتليش صورة ف ال ابديت اسيبله الصورة القديمة زي م هي  OK

			// يبقي اول حاجة اخد ال image url م البرودكت القديم 
			// واشوفها موجوده عندي ولا لا 
			// لو موجوده اخد نفس ال url بتاع الصورة القديمة واحطه عندي ف الببرودكت اللي هحفظه خلاص ف الداتا بيز
			//  ف لما يعمل ماب هتشك اذا كان اتحط url ولا لا ولو متحطش وطلع بنال هحط ال يو ار ال بتاع الصورة القديمة 
			// ولو مش موجوده عندي اممممم رجع فولس للفرونت وهو هيبلغ الكلاينت انه لازم يحط صورة بس كده 	return Ok(false);

			var PreviousImageUrl = getedProduct.pictureUrl;
			var PreviousimagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", PreviousImageUrl);
			if (!System.IO.File.Exists(PreviousimagePath) &&UpdateProductAngular.pictureUrl == null  )
			
				
				return Ok(false); //عاوزة يرجع فولس ف حالة ان الصورة القديمة مش موجوده ومش باعت صورة 

			// يبقي اول حاجة اخد ال image url م البرودكت القديم  //ok
			// واشوفها موجوده عندي ولا لا 

			// ولو مش موجوده عندي اممممم رجع فولس للفرونت وهو هيبلغ الكلاينت انه لازم يحط صورة بس كده 	return Ok(false);

			// لو موجوده اخد نفس ال url بتاع الصورة القديمة واحطه عندي ف الببرودكت اللي هحفظه خلاص ف الداتا بيز
			//  ف لما يعمل ماب هتشك اذا كان اتحط url ولا لا ولو متحطش وطلع ب استرنج فاضي هحط ال يو ار ال بتاع الصورة القديمة 
			// ف هظبط ده ف ال مابر علشان يرجع استرنج فاضل لو مفيش فايل 
			// دخلت لاقيت متظبط ف ال اوتو مابر ان لكل الاعضاء لو حاجة ب نال متعملهاش ماب
			// ف بكده هيرجع نال ف ال getedProduct.pictureUrl

			// UpdateProductAngular عاوز لو فيه قيمة جات هنا ب null ميعملهاش ماب
			mapper.Map(UpdateProductAngular, getedProduct); // هيغير القيمة اللي ف جيت برودكت ب القيمة اللي بعتهاله 
															// هنا اكني بقوله حط ف ال جيت برودكت الحاجات اللي جايه م ال ابديت ولو لاقيت حاجة ب نال سيبها زي م هي ومتعملهاش ماب 



			//جامد 

			// هيعرف البرودكت اللي انا عاوز اعمله ابديت عن طريق ال id
			unitOfWork.GetRepo<angularProducts>().UpdateItem(getedProduct);
			var result = await unitOfWork.SaveChanges();
			var UpdatedProduct = await unitOfWork.GetRepo<angularProducts>().GetByIdSpecAsync(id, spec);
			if (UpdatedProduct == null)
				return BadRequest(new HandleErrors(400, "Not Find Product In DatabaseTo Update It!"));
			var returnedProduct = mapper.Map<angularProductsDto>(UpdatedProduct);
			if (result > 0)
			{

				var CurrentImageUrl = UpdatedProduct.pictureUrl;


				await UpdateImage(PreviousImageUrl, CurrentImageUrl, UpdateProductAngular.pictureUrl);

				return Ok(returnedProduct);
			}
			else
			{
				return BadRequest(new HandleErrors(400, "Not Added"));
			}


}
			


		// لما يعمل ابديت عاوزة يحذف الصورة اللي موجوده ويحط الصورة الجديدة واعملها لواحدك

		private async Task UpdateImage(string PreviousImageUrl, string CurrentImageUrl, IFormFile? image)
		{
			if (image == null)
				return;
			// Edit Slach Of Images
			try
			{
				var PreImageEditUrl = PreviousImageUrl.Replace('/', Path.DirectorySeparatorChar);
				var CurrentImageEditUrl = CurrentImageUrl.Replace('/', Path.DirectorySeparatorChar);

				var PreImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", PreImageEditUrl);
				var CurrentImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", CurrentImageEditUrl);

				if (System.IO.File.Exists(PreImagePath)) // لو الصورة موجوده 
				{
					if (PreImageEditUrl == CurrentImageEditUrl)
					{
						return; // اخرج م الميثود 
					}
					System.IO.File.Delete(PreImagePath); //امسح اللي فاتت لو موجوده واللي جايه مش بتساوي اللي فاتت

				}
				// اضيف بقي الصورة اللي جايه 
				using var stream = new FileStream(CurrentImagePath, FileMode.Create);// كده شال الكونتينت بتاع الصورة القديمة ومستني اعمل كرييت ع الجديدة // public FileStream(string path, FileMode mode) // لو موجود هقبل كده هيعمل اوفر رايت عليها 
				await image.CopyToAsync(stream);  // اعمل كرييت بقي 

			}
			catch (Exception ex)
			{
				// هنا ممكن تسجل اللوج لو حابب تتبع المشاكل
				Console.WriteLine($"❌ Failed to delete image: {ex.Message}");
			}

		}

	






		[HttpPatch("{id}")]
		public async Task<ActionResult<angularProductsDto>> PatchProduct(int id, JsonPatchDocument<angularProducts> PatchProductAngular)
		{

			var getedProduct = await unitOfWork.GetRepo<angularProducts>().GetByIdAsync(id);// تراكدد من ال كونتيكست
			if (getedProduct == null)
			{
				return BadRequest(new HandleErrors(400, "notFound Products With This Id"));
			}


			PatchProductAngular.ApplyTo(getedProduct);


			unitOfWork.GetRepo<angularProducts>().UpdateItem(getedProduct);// ابديت لنفس الكونتيكست اذا هيعرفه ويحدثه 
			var result = await unitOfWork.SaveChanges();

			if (result <= 0)
			{
				return BadRequest(new HandleErrors(400, "Not Patched"));
			}
			var spec = new AngularProductITISpec();

			var updatedProduct = await unitOfWork.GetRepo<angularProducts>().GetByIdSpecAsync(id, spec);
			var returnedProduct = mapper.Map<angularProductsDto>(updatedProduct);

			return Ok(returnedProduct);
			/*
				 [
					 {
						"op": "replace",
						"path": "/name",
						"value": "منتج جديد"
					  },
					  {
						"op": "replace",
						"path": "/price",
						"value": 150.5
					  }
				 ]			 
			*/
		}

		[HttpGet("categories")]
		public async Task<ActionResult<IReadOnlyList<Category>>> GetITICategories([FromQuery] FilterAndSortCategories filterAndSort)
		{
			var categorySpec=new AngularITICategorySpec(filterAndSort);
			var Categories = await unitOfWork.GetRepo<Category>().GetAllSpecAsync(categorySpec);
			return Ok(Categories);
		}
		[HttpGet("categoriesInProducts")]
		public async Task<ActionResult<IReadOnlyList<Category>>> GetCategoriesInProducts()
		{
			var spec = new AngularProductITISpec();
			var products = await unitOfWork.GetRepo<angularProducts>().GetAllSpecAsync(spec);
			// عاوز اعمل ارري من ال كاتيجوري فاضية الاول وبعدين الف علي ال برودكتش واضيف ف ال اري دي ال كاتيجوريس وبعدين ارجعهم لل يوزر
			var returnedCategories=GetCategories(products);
			return Ok(returnedCategories);
		}
		private IReadOnlyList<Category> GetCategories(IReadOnlyList<angularProducts>? angularProducts)
		{
			var categoriesInProducts = new List<Category>();
			if (angularProducts == null)
				return categoriesInProducts;
				
			foreach (var product in angularProducts)
				{
					if(categoriesInProducts.Find(c=>c.Id==product.categoryId)==null)
						categoriesInProducts.Add(product.category); // جااااامد 
				}
				return categoriesInProducts;
		}
		[HttpGet("category/{id}")]
		public async Task<ActionResult<Category>> GetITICategoryById(int id)
		{
			var Category = await unitOfWork.GetRepo<Category>().GetByIdAsync(id);
			return Ok(Category);
		}
		[HttpPost("Category")]
		public async Task<ActionResult<Category>> SetITICategory(setCategoryDto setCategory)
		{
			var category = mapper.Map<Category>(setCategory);
			await unitOfWork.GetRepo<Category>().AddItemAsync(category);
			var result = await unitOfWork.SaveChanges();
			var AddedCategory = await unitOfWork.GetRepo<Category>().GetByIdAsync(category.Id);
			var returnedCategory = mapper.Map<Category>(AddedCategory);
			return result > 0 ? Ok(returnedCategory) : BadRequest(new HandleErrors(400, "Not Added"));
		}
		[HttpDelete("category/{id}")]
		public async Task<ActionResult<Category>> DeleteITICategory(int id)
		{
			var category = await unitOfWork.GetRepo<Category>().GetByIdAsync(id);
			if (category == null) { return BadRequest(new HandleErrors(400, "Not Found Category With This Id")); }
			unitOfWork.GetRepo<Category>().removeItem(category);
			var result = await unitOfWork.SaveChanges();
			return result > 0 ? Ok(category) : BadRequest(new HandleErrors(400, "Not Deleted"));
		}
		[HttpPut("category/{id}")]
		public async Task<ActionResult<angularProductsDto>> UpdateITICategory(int id, setCategoryDto UpdateCategoryAngular)
		{

			// عاوزة يحدث القيمة اللي بعتها بس واللي مبعتهاش تفضل زي م هي 

			var getedCategory = await unitOfWork.GetRepo<Category>().GetByIdAsync(id);
			if (getedCategory == null) { return BadRequest(new HandleErrors(400, "notFound Category With This Id")); }
			mapper.Map(UpdateCategoryAngular, getedCategory); // هيغير القيمة اللي ف جيت كاتيجوري ب القيمة اللي بعتهاله 

			// هيعرف البرودكت اللي انا عاوز اعمله ابديت عن طريق ال id
			unitOfWork.GetRepo<Category>().UpdateItem(getedCategory);
			var result = await unitOfWork.SaveChanges();
			var UpdatedCategory = await unitOfWork.GetRepo<Category>().GetByIdAsync(id);
			return result > 0 ? Ok(UpdatedCategory) : BadRequest(new HandleErrors(400, "Not Added"));
		}
	}
}
/*
 using var context1 = new AppDbContext();
var product = await context1.Products.FindAsync(1);

using var context2 = new AppDbContext();
context2.Products.Add(product); // هنا هيضيفه لأنه مش شايف إنه متتبع عنده  
قصدك علشان الكونتيكست ليه مرجع مختلف ف الذاكرة ف بيشوفهم مختلفين حتي لو هما نفس الكونتيكست ؟
 بالضبط يا أحمد، فهمك سليم 100% ✅

--
🔍 إزاي EF Core بيعرف إنك غيرت قيمة الـ Id؟
لما تجيب كائن من الداتا بيز مثلاً:

csharp
نسخ
تحرير
var product = await context.Products.FindAsync(5);
EF Core بيحتفظ بنسخة من الكائن ده في Change Tracker وبيسجّل القيمة الأصلية للـ Id (يعني 5).

❗ لما تغيّر product.Id = 10;
EF Core هيلاحظ إنك حاولت تغير قيمة المفتاح الأساسي، وده مش مسموح، لأنه يسبب:

تعارض في تتبع الكائن (EF مش هيعرف يربط الكائن الجديد بالكائن القديم).

خطأ أثناء محاولة حفظ التغييرات (SaveChanges).

ممكن يبوظ العلاقات مع الكيانات التانية (لو فيه Foreign Keys).

📦 داخليًا، EF Core بيعمل الآتي:
يحتفظ بالقيم الأصلية لكل خاصية في كائن داخلي اسمه ChangeTracker.

لو القيمة الأصلية هي 5، ولقاها اتغيرت لـ 10، هيعرف إنك غيرت المفتاح الأساسي.

يرمي خطأ:

"The property 'Product.Id' is part of a key and so cannot be modified or marked as modified."
 */