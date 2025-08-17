using AutoMapper;
using AutoMapper.Execution;
using Core_Layer.Models.AngularITI_Identity;

namespace ReTypeAllByMe.Mapping
{
	public class IdentityMappingITI_Identity:Profile
	{

		public IdentityMappingITI_Identity()
		{
			CreateMap<RegisterDtoITI, RegisterUser>()
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.DisplayName))//ok
				.ForMember(dest => dest.ShipAddress, opt => opt.MapFrom(src => // خلتها ليسته من اليوزر دي تو او بدل م كانت مش ليست //ok

														 new List<UserAddressDto>()
																			{
																				src.Address
																			}

				))
				.ForMember(dest => dest.PhoneNumbers, opt => //ok

				{
					opt.PreCondition(src => src.PhoneNumber != null && src.PhoneNumber.Any()); // مش هيعمل ماب الا لو الشرط ده اتحقق 
					opt.MapFrom<phoneResolver>();
				}
				)
				// عاوز اليوزر نيم تساوي ال الكلام اللي قبل الايميل 
				.ForMember(dest => dest.UserName, opt =>
				{
					
					opt.PreCondition(src => src.Email != null && src.Email.Contains('@'));
					//  public static bool IsLetterOrDigit(char c) // بيتنده ب اسم الكلاس 
					opt.MapFrom(src =>new string(src.Email.TakeWhile(c => !c.Equals('@')). // هيرجع اينبرابول من ال اتشارز لحد ال @
						Where(c=> char.IsLetterOrDigit(c))// هيرجع اينبرابول من اتشارز بتحقق الكونديشن
						.ToArray() //  هترجع ارري من اتشارز char[]
						) 
					);// هيخليها ارري من الحروف والاسترنج بياخد ارري من الحروف طبعا عادي   // هيجيب الحروف لحد م يلاقي @  هيبقي فولس ويخرج   // جامد انا اللي عاملها 
			
					
				}
				)//ok

				;


			CreateMap<UserAddressDto, ShipAddress>();
			CreateMap<DisplayNameDto, Name>()
					.ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.DisplayLastName))
					.ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.DisplayFirstName));

		}


		//    void MapFrom<TValueResolver>() where
		//    TValueResolver : IValueResolver<TSource, TDestination, TMember>;

		public class phoneResolver : IValueResolver<RegisterDtoITI, RegisterUser, ICollection<UserPhoneNumber>>
		{
			public ICollection<UserPhoneNumber> Resolve(RegisterDtoITI source, RegisterUser destination, ICollection<UserPhoneNumber> destMember, ResolutionContext context)
			{
				

					//public ICollection<string> PhoneNumber { get; set; } = null!;
					//public virtual  ICollection<UserPhoneNumber> PhoneNumbers { get; set; } = new HashSet<UserPhoneNumber>(); 
					// عاوز احول من ICollection<string>  الي ICollection<UserPhoneNumber>

				// كده هيحول ال ICollection<string>  الي ICollection<UserPhoneNumber> 
					List<UserPhoneNumber> phones = new();
					//if (source.PhoneNumber == null || !source.PhoneNumber.Any()) // عملتها فوق 
					//{

					//return phones;
					//}
					foreach (var p in source.PhoneNumber)
					{

						phones.Add(new UserPhoneNumber() { PhoneNumber = p });
					}
					return phones;
				}
			}
		}
	}

