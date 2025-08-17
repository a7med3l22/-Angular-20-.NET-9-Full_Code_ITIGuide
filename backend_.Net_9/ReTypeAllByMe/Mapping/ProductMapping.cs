using AutoMapper;
using Azure.Core;
using Core_Layer.Interfaces;
using Core_Layer.Models.Product;
using Core_Layer.Models.Product.AngularITIProducts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repository_Layer.GenericRepository.Data;
using ReTypeAllByMe.Seeding;

namespace ReTypeAllByMe.Mapping
{
	public class ProductMapping : Profile
	{
		public ProductMapping()
		{
			CreateMap<Product, ProductDto>()
				.ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
				//.ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category.Id)) // دي او اللي تحتها عادي 
				.ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
				.ForMember(dest => dest.ProductBrand, opt => opt.MapFrom(src => src.Brand.Name))
				.ForMember(dest => dest.BrandId, opt => opt.MapFrom(src => src.BrandId))

				.ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<PictureResolver>()); // Handle PictureUrl 

			/*
			 
			 .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category.Id)) 
			بيعمل كده عادي 
			اما العكس لا لأانه الاول سهل ع الاوتو مابر انه يعمله التانيه معقده 
							.ForMember(dest => dest..Category.Id, opt => opt.MapFrom(src => src.CategoryId)) 
			 */
			//CreateMap<ProductsJsonDto, Product>()
			// كل اللفه دي علشان انا مش عاوز احط ال CategoryId,BrandId ف ال برودكت 
			// عاوزة يقرأ القيم من كاتيجوري اللي ف الداتا بيز معملش نيو كاتيجوري لاني عاوزة يتبع الكاتيجوري اللي ف الداتا بيز مش نيو من عندي
			//.ForMember(dest => dest.Category, opt => opt.MapFrom<categoryResolver>())
			//.ForMember(dest => dest.Brand, opt => opt.MapFrom<brandResolver>())
			;
			/*
					تتبروز دي المشكلة اللي مخلتنيش اعرف اضيفهم ف الداتا بيز ;>
			//		// المشكلة هنا -- ان النسخة اللي جايه من ال ApplicationDbContext غير النسخة اللي مستخدمة ف الاضافة لاحقا 
			//		// ف هو هيعرف ان البراند ده جاي من كونتيكست غير الكونتيكست اللي هينضاف ليه ف بالتالي لما يضيفه هيتعامل معاه ع انه براند جديد مش براند اللي ف نفس الكونتيكست اللي هو هينضاف ليه  
			 
			 */

			/*
		

							 
			 
			 
			 */

			// عملت كده علشان عاوز لما اضيف برودكت ف ال كونتيكست اضيفه من غير م اكون معرف كاتيجوري اي دي و براند اي دي 
			/*
							context.DeliveryMethods.add(); //اي دي زي م قولت  عاوز اضيف هنا برودكت من غير معرف كاتيجوري  اي دي و براند
							وطبعا ده شكل الداتا بيز بتاع البرودكت 
							Id,Name,Description,Price,PictureUrl,CategoryId,BrandId
			ف انا عاوز ابعتله ال CategoryId,BrandId لما اضيف البرودكت 
			ف فيه طريقتين 
			اول طريقة اني لما ابعت برودكت يكون متعرف فيه CategoryId,BrandId برويبيرتي
			ولو مفهوش وعاوزة يحط في الداتا بيز القيم دي يبقي ياخدها من ال الكتيجوري وال براند ويكونوا جايين من نفس النسخة بتاعت ال دي بي كونتيكيست وهينضافوا ف نفس النسخه علشان يعرف انهم مرتبطين ببعض ويعمل ريليشن بينهم     
			....
			ولما احتاج اجيب القيم م الداتا بيز اعمل انكلود للبراند لو انا عاوزة وهيجبهولي لان معاه ال اي دي بتاعه اللي بعتهوله ب الطريقة دي لان الفورين كي بتاعها متسجل ف البرودكت وفيه علاقه بينهم وان ناحيه ال براند وميني ناحيه البرودكت

			 */
			///////////////////



		}
	}
	//internal class categoryResolver : IValueResolver<ProductsJsonDto, Product, Category>
	//{
	//	private readonly IServiceProvider serviceProvider;

	//	//public categoryResolver(ApplicationDbContext dbContext)  // مينفعش اعمل كده لاني مينفعش احقن خدمة اسكوب جوه خدمة سينجيلتون
	//	//{
	//	//	this.dbContext = dbContext; 
	//	//}
	//	public categoryResolver(IServiceProvider serviceProvider)
	//	{
	//		this.serviceProvider = serviceProvider;
	//	}

	//	public Category Resolve(ProductsJsonDto source, Product destination, Category destMember, ResolutionContext context)
	//	{
	//		using var scope = serviceProvider.CreateScope(); 
	//		var dbContext=scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

	//		var category = dbContext.Categories.Find(source.CategoryId);

	//		return category!;
	//	}
	//}
	//internal class brandResolver : IValueResolver<ProductsJsonDto, Product, Brand>
	//{
	//	private readonly IServiceProvider serviceProvider;

	//	//public brandResolver(ApplicationDbContext dbContext)  // مينفعش اعمل كده لاني مينفعش احقن خدمة اسكوب جوه خدمة سينجيلتون
	//	//{
	//	//	this.dbContext = dbContext; 
	//	//}
	//	public brandResolver(IServiceProvider serviceProvider)
	//	{
	//		this.serviceProvider = serviceProvider;
	//	}

	//	public Brand Resolve(ProductsJsonDto source, Product destination, Brand destMember, ResolutionContext context)
	//	{
	//		using var scope = serviceProvider.CreateScope();
	//		var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>(); 
	//		// المشكلة هنا -- ان النسخة اللي جايه من ال ApplicationDbContext غير النسخة اللي مستخدمة ف الاضافة لاحقا 
	//		// ف هو هيعرف ان البراند ده جاي من كونتيكست غير الكونتيكست اللي هينضاف ليه ف بالتالي لما يضيفه هيتعامل معاه ع انه براند جديد مش براند اللي ف نفس الكونتيكست اللي هو هينضاف ليه  
	//		var brand = dbContext.Brands.Find(source.BrandId);

	//		return brand!;
			
	//	}
	//}
	internal class PictureResolver : IValueResolver<Product, ProductDto, string>
	{
		private readonly IApiUrlProvider _apiUrlProvider;

		public PictureResolver(IApiUrlProvider apiUrlProvider)
		{
			_apiUrlProvider = apiUrlProvider;
		}
		public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
		{
			var apiUrl = _apiUrlProvider.GetApiUrl();
			return apiUrl+source.PictureUrl;

		}
	}

}
