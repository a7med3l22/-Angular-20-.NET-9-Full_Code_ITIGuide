using AutoMapper;
using Core_Layer.Interfaces;
using Core_Layer.Models.AngularITI_Identity;
using Core_Layer.Models.ErrorsHandle;
using Core_Layer.Models.Identity;
using Core_Layer.Specefication;
using Core_Layer.Specification;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.StaticFiles.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Repository_Layer.AngularITI;
using Repository_Layer.AngularITI.AngularUnitOfWork;
using Repository_Layer.AngularITI_Identity;
using Repository_Layer.GenericRepository.CahceRepo;
using Repository_Layer.GenericRepository.Data;
using Repository_Layer.GenericRepository.Data.Configurations.AngularITI_IdentityConvigrations;
using Repository_Layer.IdentityRepository;
using Repository_Layer.UnitOfWork;
using ReTypeAllByMe.CustomMiddleWare;
using ReTypeAllByMe.Mapping;
using ReTypeAllByMe.Seeding;
using Service_Layer;
using Service_Layer.JwtToken;
using Service_Layer.Order_Service;
using Service_Layer.Services;
using StackExchange.Redis;
using System.Data.Common;
using System.Text;
using static StackExchange.Redis.Role;


namespace ReTypeAllByMe
{
	/*
	 *  الخلاصة ان احنا بنعمل كرييت اسكوب للخدمات المتسجلة ك اسكوب علشان مش بتبقي مانجد خارج ال اتش تي تي بي ريكويست بايبلاين ف احنا بنعمل كرييت اسكوب علشان لما نستخدمها خارج ال ريكويست يتعمل للخدمة دي ديسبوز لما الاسكوب ينتهي 
	 */



	public static class ExtensionalCode
	{
		//دوت دي معناها ان هات الحاجات اللي جوا الحاجة دي سواء كانت الحاجة دي كلاس او انترفيس او استراكت/////✔///////
		// (this IServiceCollection app)
		// اكني ب الظبط عملت الميثود دي جوه ال اي سيرفس كوليكشن/////✔///////
		///معناها ببساطة ان اي حاجة من نوع ال الانترفيس ده لو عملت بعدها دوت هيظهرلي الميثود دي ماي بيلدر كود
		//Return IServiceCollection
		/// عملت كده ببساطة علشان لو عملت دوت بعد الميثود دي ماي بيلدر كود يظهرلي اي ميثود جوه الانترفيس ده او اي اكستنشن ميثود من الانترفيس ده
		//مثلا -- builder.Services دي من نوع IServiceCollection 
		//ف لو عملت بعدها دوت هيجيبلي اي حاجة جوه ال IServiceCollection
		//public static void test(Action<int> d)
		//{

		//}
		//public static void testAction(Action<FilterAndSortProduct> dD)// مهم جدا هيفهمك الدنيا شغاله ازاي ف ال ديليجيت
		//{
		//	var filterAndSortProduct = new FilterAndSortProduct() { SortBy = "xxx", FilterByCategoryIdNumber = 55 };
		//	dD.Invoke(filterAndSortProduct);

		//}
		//public static void testFunc(Func<FilterAndSortProduct> dD)// مهم جدا هيفهمك الدنيا شغاله ازاي ف ال ديليجيت
		//{
		//	var x = dD.Invoke();
		//	Console.WriteLine(x.SortBy);
		//}
		//public static void testFunc2(Func<FilterAndSortProduct, FilterAndSortProduct> dD)// مهم جدا هيفهمك الدنيا شغاله ازاي ف ال ديليجيت
		//{
		//	var filterAndSortProduct = new FilterAndSortProduct() { SortBy = "xyz", FilterByCategoryIdNumber = 45 };
		//	var x = dD.Invoke(filterAndSortProduct);
		//	Console.WriteLine(x.SortBy+x.FilterByCategoryIdNumber+x.FilterByProductName);//SortBy = "alaa",FilterByCategoryIdNumber=null ,FilterByProductName = null
		//	Console.WriteLine(filterAndSortProduct.SortBy+ filterAndSortProduct.FilterByCategoryIdNumber+ filterAndSortProduct.FilterByProductName);//SortBy = "ss", FilterByCategoryIdNumber = 45 ,FilterByProductName = "ali" 

		//}
		public static IServiceCollection MyBuilderCode(this IServiceCollection services, IConfiguration config)
//Gold	// حطيت this قبل IServiceCollection علشان أحوّل الدالة لـ Extension Method
		// وده بيخليني أقدر أستخدمها كأنها دالة تابعة لأي كائن من نوع IServiceCollection
		// وبالتالي بدل ما أكون مجبر أبعت البراميترين بشكل صريح، هابعت بس IConfiguration
		// أما IServiceCollection فهي بتتبعت ضمنيًا لما أستخدم الدالة بالدوت على كائن من نوع IServiceCollection
		// ملاحظة: البراميتر اللي فيه this لازم يكون أول براميتر في الـ Extension Method


		{
			//test((int x)=> Console.WriteLine(x));
			//test((x) => Console.WriteLine(x));

			//testAction(x => { x.SortBy = "name"; }); // مهم جدا هيفهمك الدنيا شغاله ازاي ف ال ديليجيت
			//testFunc(() => { return new FilterAndSortProduct { SortBy = "name" }; });// مهم جدا هيفهمك الدنيا شغاله ازاي ف ال ديليجيت
			//testFunc2((x) => { x.FilterByProductName = "ali"; x.SortBy="ss"; return new FilterAndSortProduct { SortBy = "alaa" }; });// مهم جدا هيفهمك الدنيا شغاله ازاي ف ال ديليجيت

			//services.AddCors( CorsOptions => CorsOptions.AddPolicy("AllowAngular",
			//services.AddCors((CorsOptions CorsOptions) => CorsOptions.AddPolicy("AllowAngular",

			services.AddCors((CorsOptions CorsOptions) => CorsOptions.AddPolicy("AllowAngular",
				(CorsPolicyBuilder configuePolicy) => configuePolicy.AllowAnyMethod().AllowAnyHeader().WithOrigins(config["AngularLocalHost:AngularVsCode"]!, config["AngularLocalHost:AngularIIS"]!)));
			// Add services to the container.
			services.AddControllers().AddNewtonsoftJson(
				 JsonOptions =>
				 {
					 JsonOptions.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; //ReferenceLoopHandling.Ignore يعمل فقط أثناء Serialization (أي أثناء تحويل الـ object إلى JSON 
				 }
				);
			services.AddTransient<HandelErrorMiddleWare>();

			services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(config.GetConnectionString("DefaultConnection"))/*.UseLazyLoadingProxies()*/);

			services.AddDbContext<ApplicationIdentityDbContext>(option => option.UseSqlServer(config.GetConnectionString("IdentityConnection")));

			services.AddDbContext<AngularDBContext>(opt => opt.UseSqlServer(config["ConnectionStrings:AngularConnection"],opt=>opt.CommandTimeout(360)));

			//services.AddDbContext<IdentityContext>(option => option.UseSqlServer(config.GetConnectionString("IdentityConnectionITI"), opt => opt.CommandTimeout(360)));// علشان يطول المده وانا بعمل مايجريت
			services.AddDbContext<IdentityContext>(option => option.UseSqlServer(config.GetConnectionString("IdentityConnectionITI")));// علشان يطول المده وانا بعمل مايجريت
			/*
			 📌 هنا يحصل:
				 يقوم ASP.NET Core بإنشاء DbContextOptions<ApplicationIdentityDbContext> تلقائيًا خلف الكواليس.
				 يقوم بتمرير الـ Connection String وإعدادات SQL Server التي قمت بتحديدها داخل:
				options.UseSqlServer(...)
			 */

			services.AddScoped<IUnitOfWork,UnitOfWork>(); 
			//🔹 هنا تقوم بتسجيل GenericRepository في DI Container، مما يسمح لك باستخدامه في أي مكان في التطبيق.
			//ولما اطلب من ال DI خدمة من نوع IGenericRepository هيديني نسخة من GenericRepository 
			// والنسخة اللي هيدهالي هتعيش معايا طول فترة الريكويست بايب لاين بعد كده النسخة دي هتتمسح تلقائي لانها بير ريكويست
			services.AddHttpContextAccessor();
			services.AddScoped<IApiUrlProvider, ApiUrlProvider>(); //🔹 هنا تقوم بتسجيل خدمة توفر عنوان الـ API في DI Container.

			services.AddScoped<IProductService, ProductService>();
			services.AddScoped<IOrderService, OrderService>();


			services.Configure<ApiBehaviorOptions>(options=>
			options.InvalidModelStateResponseFactory=
			context =>
			{
				var errors = context.ModelState
							.Where(e => e.Value!.Errors.Count > 0)
							.SelectMany(e => e.Value!.Errors)
							.Select(e => e.ErrorMessage)
							.ToList();

				return new BadRequestObjectResult(new ValidationErrors(errors));
			}

			);

			services.AddSingleton<IConnectionMultiplexer>(serverProvider =>
			{
				var redisConnection=serverProvider.GetRequiredService<IConfiguration>()["Redis:RedisConnectionString"]; //🔹 هنا تقوم بالحصول على IConfiguration من DI Container.
				return ConnectionMultiplexer.Connect(redisConnection!);
			}); //🔹 هنا تقوم بتسجيل خدمة Redis في DI Container.
				// لما اطلب من ال DI خدمة من نوع IConnectionMultiplexer هيديني نسخة من ConnectionMultiplexer  
				// والنسخة دي هتعمل كونيكت مع ال ريدس وهتفضل عايشة طول عمر التطبيق ب التالي هيفضل معمول كونكت مع ال ريدس طول عمر التطبيق
			services.AddScoped<IBasketService, BasketService>();
			services.AddScoped<IPaymentService, PaymentService>();
			services.AddScoped<ICacheRepo, CacheRepo>();
			services.AddScoped<IUnitOfWorkAngular,UnitOfWorkAngular>();
			services.AddAutoMapper(IMapperConfig=> IMapperConfig.AddMaps(
				typeof(BasketMapping).Assembly
				)
			);
			//🔹 هنا تقوم بتسجيل AutoMapper في DI Container.

			//opt => opt.User.RequireUniqueEmail = true// الانشيال ب فولس
			services.AddIdentity<RegisterUser, IdentityRole>(opt => opt.User.RequireUniqueEmail = true)
				.AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();


			// مينفعش اعملها مرتين AddIdentity

			//services.AddIdentity<ApplicationUser, IdentityRole>(
			//	IdentityOptions=> { IdentityOptions.User.RequireUniqueEmail = true; } //UserName : فريد افتراضيًا دائمًا.
			//	) //✅ يقوم بتسجيل خدمات الهوية Identity داخل الـ DI Container (مثل UserManager, SignInManager, RoleManager). // علشان لو عوزت UserManager<ApplicationUser> userManager يحقنهالي
			//		.AddEntityFrameworkStores<ApplicationIdentityDbContext>()//✅ المستخدمين والأدوار سيتم حفظهم في جداول AspNetUsers, AspNetRoles, AspNetUserRoles داخل قاعدة البيانات التي يديرها ApplicationDbContext.
			//		.AddDefaultTokenProviders();//✅ يضيف مزودي التوكينات الافتراضيين إلى الهوية لدعم:توكين تأكيد البريد الإلكتروني.توكين استعادة كلمة المرور.توكين تأكيد رقم الهاتف.

			//services.AddScoped<IJwtToken, JwtToken>();
			services.AddScoped<IJWT_ITI, JWT_ITI>();

			services.AddAuthentication(
				options =>
				{
					options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
					options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				}
				).AddJwtBearer(                //AddJwtBearer(this AuthenticationBuilder builder, Action<JwtBearerOptions> configureOptions)
											   //اول لما يدخل هنا ال بيلدر دي بتكون ال ريتيرن اللي راجع م الميثود اللي قبلها this AuthenticationBuilder builder
											   //هنا ببعتله configureOptions  من نوع      Action<JwtBearerOptions> configureOptions) يعني ببعتله فانكشن بتاخد براميتر JwtBearerOptions ومش بترجع حاجة
											   //فهو داخليا بقي بيستقبل الفانكشن دي وبعد كده يعمل انفوك ليها بينفذها يعني ف ب قوم باعتلها اوبجيكت من  JwtBearerOptions 
											   // وبعدين يدخل جوه الفانكشن ف ينفذ الحاجات اللي جواها بقي و بعد م ينفذ اللي جوه الفانكشن هنا يقوم راجع تاني جوه الميثود دي والتغييرات اللحصلت ف ال اوبجيكت اللي بعته ف البارميتر جوه الفانكشن بتتغير طبعا ف الاوبجيكلما يرجع بتفضل موجوده اكيد لانها ريفرينس تايب بيغير ف اللي بعته شخصيا ده كلام مفروغ منه 
											   // تمام عاش جدا 

				options =>
				
				options.TokenValidationParameters = new ()// ف البداية عبارة عن اوبجيكت فاضي ف انا حطيت فيه القيم ب الطريقة دي بدل م اعمل دوت ل كل حاجة  //public TokenValidationParameters TokenParameters { get; set; } = new TokenValidationParameters(); // وبدل م كان بيساوي اوبجيكت فاضي انا خليته يساوي اوبجيكت فيه اللي انا هحطها دي 
				{

		//👏 شاطر يا أحمد إنك فتحت الكود المصدر بنفسك، وهذا سلوك مهندس حقيقي. //👏👏 صح يا أحمد، وحقك عليّ!✅ أنت فعلًا الصح هنا لأنك فتحت الكود المصدر بنفسك و تحققت بعينك، ودي أفضل طريقة لتصبح مبرمج محترف.

					ValidateIssuerSigningKey = true, // خليت دي ب ترو علشان اعرفه اني عملت اوبجيكت من ال سيكيورتي كي  // عملت ده بس اللي ترو لان الباقي معمول ترو ف الديفولت
					ValidIssuer = config["Jwt:Issuer"], // هيساويها ب اللي موجود هنا 
					ValidAudience = config["Jwt:audience"],// هيساويها ب اللي موجود هنا 
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!)), // IssuerSigningKey كانت ب نال ف خليتها كده 
					ClockSkew = TimeSpan.Zero //✅ يضيف ClockSkew = TimeSpan.Zero لمنع قبول التوكن بعد انتهاء صلاحيته حتى ولو بثوانٍ.

				}
				);
			return services;
		}
		//دوت دي معناها ان هات الحاجات اللي جوا الحاجة دي سواء كانت الحاجة دي كلاس او انترفيس او استراكت/////✔///////
		// (this WebApplication app)
		// اكني ب الظبط عملت الميثود دي جوه ال ويب ابلكيشن كلاس/////✔///////
		///معناها ببساطة ان اي حاجة من نوع ال كلاس ده لو عملت بعدها دوت هيظهرلي الميثود دي ماي اب كود
		//Return WebApplication
		///عملت كده ببساطة علشان لو عملت دوت بعد الميثود دي ماي بيلدر كود يظهرلي اي ميثود جوه الكلاس ده او اي اكستنشن ميثود من الكلاس ده

		//الموضوع لما بيتفهم بيبقي بسيط جدا

		public async  static Task<WebApplication> MyAppCode(this WebApplication app)
		{
			app.UseCors("AllowAngular");
			app.UseMiddleware<HandelErrorMiddleWare>(); //🔹 هنا تقوم بتسجيل Middleware لمعالجة الأخطاء في التطبيق.

			//var mapper =app.Services.GetRequiredService<IMapper>(); // عملتها بره الاسكوب عادي لانها مدارة بواسطة ال DI CONTAINER 
			app.UseStatusCodePagesWithReExecute("/error/{0}");
			
			using var scope = app.Services.CreateScope();

		
			/*
			 *  الكونستركتور بتاع الكنترولر بيتنفذ مع كل ريكويست تمام 
			 *  وطبعا مع كل مرة بيطلب من ال DI الحاجات اللي حقنتها ف الكونستركتور
			 *  وال DI لو طلبت منه حاجة بيفضل حافظها عنده ب لايف تايم معين
			 *  ف لو طلبت منه خدمة سينجيلتون بيفضل حافظها جواه طول فتره التطبيق ولو طلبتها منه تاني هيديك نفس النسخه دي
			 *  لو طلبت منه خدمة ترانسينت بيديك خدمة جديدة كل مرة تطلب فيها منه 
			 *  لو طلبت منه خدمة سكوبد بيفضل حافظها جواه طول فترة ال سكوب اللي هو معمول فيه ولو طلبتها منه اكتر من مرة ف نفس ال scope هيديك نفس الخدمة
			 */
			/*
			 *			لو جبت خدمة مسجلة ك Scoped ف ال DI لازم اعملها انا create scope  حتي تدار بواسطتي لان ال scope اللي هي معمولة بيه خاص ب ال http pipeline فقط
			 */
			/*		using var scope = app.Services.CreateScope();
					لو انا جبت خدمة ترانسيت من الاسكوب ده والخدمة دي كانت بتعمل امبليمسنت ل idisposable ف بيتم عمل dispose ليها عند انتهاء الاسكوب ده 
			*/
			var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>(); //🔹 فأنت هنا تطلب أي خدمة مسجلة داخل الـ DI Container للتطبيق.
				//var identityDbContext = scope.ServiceProvider.GetRequiredService<ApplicationIdentityDbContext>(); //🔹 هنا تطلب خدمة الـ Identity DbContext.
				var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("ExtensionalCode:MyAppCode"); //🔹 هنا تطلب خدمة الـ Logger.
				var angularITIContext = scope.ServiceProvider.GetRequiredService<AngularDBContext>();
				var angularITIContextIdentity = scope.ServiceProvider.GetRequiredService<IdentityContext>();

			try
			{
				await AddSeedDataToAngularITI.AddSeedData(angularITIContext);
				//await identityDbContext.Database.MigrateAsync(); //🔹 هنا تقوم بتطبيق أي ترحيلات قاعدة البيانات (Migrations) الخاصة بالهوية (Identity) إذا كانت موجودة.
				await dbContext.Database.MigrateAsync(); //🔹 هنا تقوم بتطبيق أي ترحيلات قاعدة البيانات (Migrations) إذا كانت موجودة.
				await angularITIContext.Database.MigrateAsync();
				await angularITIContextIdentity.Database.MigrateAsync();
				//await AddSeedDataToDataBase.AddSeedData(dbContext, mapper); //🔹 هنا تقوم بإضافة بيانات ابتدائية إلى قاعدة البيانات.
				await AddSeedDataToDataBase.AddSeedData(dbContext); //🔹 هنا تقوم بإضافة بيانات ابتدائية إلى قاعدة البيانات.

				logger.LogInformation("Database migration and seeding completed successfully.✔");
				}
				catch (Exception ex) 
				{
					logger.LogError(ex, "An error occurred while seeding the database Or While Migrate Database.🎇"); //🔹 هنا تقوم بتسجيل الخطأ في الـ Logger.
				}
			//finally
			//{
			//	scope.Dispose();   // لما يحصل ديسبوز لل اسكوب معناها ان اي خدمة اتجابت من الاسكوب ده هينادي علي ال ايديسبيزبول بتاعها علشان يعملها ديسبوز
			//}

			// StaticFileOptions(SharedOptions sharedOptions)

			app.UseStaticFiles(new StaticFileOptions() // كده مش هتتحفظ ف الكاش 
			{
				// علشان ميحفظهومش ف الكاش
				// Action<StaticFileResponseContext> OnPrepareResponse { get; set; }
				OnPrepareResponse = (StaticFileResponse) =>
				{
					//StringValues ✅ StringValues بيدعم implicit conversion من string و string[].
					StaticFileResponse.Context.Response.Headers.Append("Cache-Control", "no-cache, no-store");
					StaticFileResponse.Context.Response.Headers.Append("Pragma", "no-cache");
					StaticFileResponse.Context.Response.Headers.Append("Expires", "-1");
				}
			});
			
			
			return app;
		}

	}
}
/*
 

 	Func<ActionResult, IActionResult> ex = AR =>
			{
				return new BadRequestObjectResult("aa");
			}
			;

				ممكن اعمل كده او كده  لو هي سطر واحد بس


			Func<ActionResult, IActionResult> ex = AR => new BadRequestObjectResult("aa");
			بياخد باراميتر من النوع ActionResult.
            بيرجع قيمة من النوع IActionResult.

							///////////////////
							
		Func<ActionResult, IActionResult> ex= AR => new BadRequestObjectResult("aa"); // هي طريقة قصيرة لكتابة دالة 

			//Or

			Func<ActionResult, IActionResult> ex = func;

			IActionResult func(ActionResult AR)
			{
				return new BadRequestObjectResult("aa");
			}

example:-
	Action<int> actionn = a=> Console.WriteLine("dd"); 
			Action<int> action = aa;

			void aa(int a)
			{
				Console.WriteLine("dd");
			}

			action.Invoke(5);
			actionn.Invoke(5);

example:-

		Func<RegisterDto, string> src = func;
				

				string func(RegisterDto value)
				{
					return value.Email.Substring(0, value.Email.IndexOf("@"));
				}

			var registerDto=new RegisterDto
			{
				Email = "ahmedalaa@gmail.com"
			};
			Console.WriteLine(src.Invoke(registerDto));
 
 */