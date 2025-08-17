using AutoMapper;
using Core_Layer.Models.Identity;
using ReTypeAllByMe.Dto;
using System;
using System.Linq.Expressions;

namespace ReTypeAllByMe.Mapping
{
	public class IdentityMapping : Profile
	{
		public IdentityMapping()
		{
			CreateMap<RegisterDto, ApplicationUser>()
				.ForMember(dest => dest.UserName,
					opt => opt.MapFrom(src => src.Email.Substring(0, src.Email.IndexOf("@"))));
			CreateMap<AddressDto, Address>().ReverseMap();

		}
	}
}
