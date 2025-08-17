
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ReTypeAllByMe.CustomMiddleWare;

namespace ReTypeAllByMe
{
	// برنس والله 
	/*
			سينجيلتون يعني لما اطلب يسرفس من ال DI هيبعتلي نسخة منها والنسخة دي هتفضل محفوظه ف ال DI Container طول فترة التطببيق ولو طلبتها تاني عن طريق اني احقنها طبعا ف اي كونستراكتور تاني هيبعتلي نفس النسخة 
			اسكوبد يعني لما اطلب سيرفس من ال DI هتفضل محفوظة في ال DI Container طول عمر الريكويست ولو حقنتها ف اي كونستراكتور تاني هيديني نفس النسخة لو نفس الريكويست , 
			ومن هنا لو انا عاوز اطلب خدمة مش هيحقنهالي ال DI ف الكونستراكتور مش هتبقي مانجيد ب ال DI طبعا ف ب التالي لازم انشأ انا اسكوب بنفسي بحيث ان لما ال اسكوب يخلص يعمل ديسبوز للخدمات اللي طلبتها م الاسكوب ده  
			والترانسيت لما بطلب من ال DI خدمة بيديني نسخة جديدة ف كل مرة بحقن فيها الخدمة دي 
	// ملحوظة مهمة جدا -- اي خدمة محقونه ف الكوستراكتور وال DI هو اللي هيحقنهالي وهيبعتلي الخدمة دي هتبقي مانجيد بواسطته غير كده هتبقي مانجيد بواسطتي انا 
	و ب التالي لازم اعملها انا ديسبوذ ب ايدي لو الخدمة دي بتستخدم IDIsbosable سواء ب استخدام using او dispose ب ايدي ولو معملتلهاش ديسبوذ يبقي مفيش فايدة اني اعملها اسكوب بقي لو هي اسكوبد واجيبها م الروت  ع طول بس ده غلط طبعا ولازم استخدم using او dispose  علشان يبقي فيه فرق بين اني اجيبها م الروت و م الاسكوب الفرق ده هيبان لو استخدمت يوزنج 
	 
		var x=app.Services.GetRequiredService<IJwtToken>(); // مش هعرف اعملها dispose 

			var y = app.Services.CreateScope(); //انما هنا هعرف 
			var z= y.ServiceProvider.GetRequiredService<IJwtToken>();
			y.Dispose();
	 */
	public class Program
    {
        public async static Task Main(string[] args)
        {
			var builder = WebApplication.CreateBuilder(args);



			// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
			// builder.Services.AddOpenApi();
			builder.Services.AddEndpointsApiExplorer();//توفر تفاصيل الـ Endpoints لـ Swagger.
			builder.Services.AddSwaggerGen();// Enable Swagger generation
			builder.Services.MyBuilderCode(builder.Configuration); //My Builder Code 
			//ExtensionalCode.MyBuilderCode(builder.Services, builder.Configuration);
			var app = builder.Build();

			await app.MyAppCode();
			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
            {
                //app.MapOpenApi();
                app.UseSwagger();// Enable Swagger generation

			}
          
         
            app.UseHttpsRedirection();
            app.UseSwaggerUI(); // Enable Swagger UI
			app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}


/*
        ممكن اعملها كده 
                    Action<MvcNewtonsoftJsonOptions> JsonOptions =
                    JsonOptions =>
                    {
                        JsonOptions.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
				    };
			
			    // Add services to the container.
			    builder.Services.AddControllers().AddNewtonsoftJson(
				    JsonOptions
				    );

او كده اللي يريحني 

	// Add services to the container.
			builder.Services.AddControllers().AddNewtonsoftJson(
				 JsonOptions =>
				 {
					 JsonOptions.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
				 }
				);

او كده الللي يريحك بردو	 
		void jsonOptins(MvcNewtonsoftJsonOptions newtonsoftJsonOptions)
				{
					newtonsoftJsonOptions.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
				}

				Action<MvcNewtonsoftJsonOptions> JsonOptions = jsonOptins;


				// Add services to the container.
				builder.Services.AddControllers().AddNewtonsoftJson(
					JsonOptions
					);

 */