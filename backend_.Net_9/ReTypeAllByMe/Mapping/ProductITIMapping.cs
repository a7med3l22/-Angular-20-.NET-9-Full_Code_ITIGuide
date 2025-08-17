using AutoMapper;
using AutoMapper.Execution;
using AutoMapper.Internal;
using Core_Layer.Interfaces;
using Core_Layer.Models.Product;
using Core_Layer.Models.Product.AngularITIProducts;
using Service_Layer;
using System.Linq.Expressions;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;

namespace ReTypeAllByMe.Mapping
{
	public class ProductITIMapping : Profile
	{
		 // مهم جدا // أي حاجة  استاتيك بتتتحمل ف الذاكرة اول م يتم استخدامه وبيخليه عايش طو لعمر التطبيق



		public ProductITIMapping()
		{
			CreateMap<angularProducts, SetProductAngular>()
				.ReverseMap().ForMember(dest => dest.pictureUrl,
				opt =>
				{
					opt.MapFrom(src => "imagesITI/products/" + Path.GetFileName(src.pictureUrl.FileName));
				}

				)

				;

			CreateMap<angularProducts, UpdateProductAngular>()
				.ReverseMap()
				.ForMember(dest=>dest.pictureUrl,
				opt=>
				{
					// جامد
					opt.Condition(src => src.pictureUrl != null); // لو null مش هيعمل ماب
					opt.MapFrom(src => "imagesITI/products/" + Path.GetFileName(src.pictureUrl!.FileName));
				}
				
				) // أمان ضد اسم الملف الخبيث//image.FileName=>Path.GetFileName(image.FileName) <><> "C:\\Users\\ahmed\\Downloads\\x.png"  =>	"x.png"
				//	void Condition(Func<TSource, TDestination, TMember, bool> condition);

				// جامد بقوله لو السورس ميمبر انتيجر نفذ ده ولو ديسيمال نفذ ده ولو غيرهم نفذ ده
				.ForAllMembers(opts => opts.Condition((src,
				dest,
				srcMember // اوبجيكت من ال كلاس سورس سواء كان اي حاجة فيه  
				) => // لو فولس مش هيعمل ماب
				{
					{
						if (srcMember == null)
							return false; // يعني متعملش ماب

						/*
		 
									بص لما اعمل كده 
								int x;
						ال x هتساوي صفر 
						لان كده بتساوي كده 
						int x=new int();
						وال new int() قيمتها متخزنة ب صفر 

						 */
						var type = srcMember.GetType();//int   
													   // ✅ دعم الـ Nullable Types (زي int? أو decimal?)
						if (Nullable.GetUnderlyingType(type) != null) //بيقولك "هل النوع ده Nullable؟ لو آه، رجع لي النوع الحقيقي اللي جواه"
							type = Nullable.GetUnderlyingType(type); // يعني لو type هو int?, بيخليه int. ولو decimal?, بيخليه decimal

						if (type == typeof(string))
						{
							if (string.IsNullOrWhiteSpace((string)srcMember))
								return false;

							return true;
						}


						/*
						 Activator.CreateInstance(type)
						1- لو ال تايب int  هيعمل كده  
						 new int() // وده هيرجع 0
						1- لو ال تايب string  هيعمل كده  
						new string();
						وهيقوم ضارب اكسيبشن لان ال استرنج مفهوش كنستراكتور فاضي 
						 */

						if (type != null && type.IsValueType)
						{
							if (srcMember.Equals(Activator.CreateInstance(type)))
								return false;
							return true;
						}
						return false;


						/*
						if (srcMember == Activator.CreateInstance(type)) return false;
						و

						if (srcMember.Equals(Activator.CreateInstance(type))) return false;
						يبدو إنهم بيعملوا نفس الحاجة، لكن فيه فرق مهم 👇

						✅ الفارق الأساسي:
						==
						عامل المقارنة (==) بيتم ترجمته حسب نوع المتغير.

						لو srcMember هو object (زي ما هو فعلاً)، فالمقارنة هتكون مرجعية (reference comparison).

						يعني هيقارن: "هل الاتنين بيشيروا لنفس المكان في الذاكرة؟"

						.Equals(...)
						دي مقارنة محتوى.

						بتقارن بين قيمتين حسب نوعهم الفعلي.

						int.Equals هيقارن القيمة الرقمية صح.

						string.Equals هيقارن محتوى النص صح.

						وهكذا...

						*/


					}
					//if (srcMember == null)
					//	return false;

					//if (srcMember is int intVal && intVal == 0)
					//	return false;

					//if (srcMember is decimal decimalVal && decimalVal == 0)
					//	return false;

					//// لو وصلت هنا يبقى فيه قيمة تستحق الماب
					//return true;
				}));
			CreateMap<angularProducts, angularProductsDto>()
				.ForMember(dest => dest.pictureUrl, opt => opt.MapFrom<pictureResolver>())
				.ForMember(dest => dest.category, opt => opt.MapFrom(src => src.category != null ? src.category.Name : null));
			//.ReverseMap();




			CreateMap<setCategoryDto, Category>();

		}
		public class pictureResolver : IValueResolver<angularProducts, angularProductsDto, string>
		{
			private readonly IApiUrlProvider apiUrlProvider;

			public pictureResolver(IApiUrlProvider apiUrlProvider)
			{
				this.apiUrlProvider = apiUrlProvider;
			}
			public string Resolve(angularProducts source, angularProductsDto destination, string destMember, ResolutionContext context)
			{
				return apiUrlProvider.GetApiUrl()+source.pictureUrl;
			}
		}
	}
}
