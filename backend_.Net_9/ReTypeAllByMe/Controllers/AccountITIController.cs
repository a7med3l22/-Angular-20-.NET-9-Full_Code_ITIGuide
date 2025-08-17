using AutoMapper;
using Core_Layer.Interfaces;
using Core_Layer.Models.AngularITI_Identity;
using Core_Layer.Models.ErrorsHandle;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer.AngularITI_Identity;
using ReTypeAllByMe.Dto;
using Stripe;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ReTypeAllByMe.Controllers
{
	//الافضل اني اقول ان المثود بتاخد انستانس من كلاس كذا او انستانس من استراكت كذا .. دي اصح حاجة !
	// مثال 
	//x(int xx) اقول انها بتاخد انستانس من استراكت int مش بتاخد استراكت int دي اصح حاجة
	//x(product zz)  بتاخد انستانس من كلاس برودكت

	public class AccountITIController : BaseApiController
	{
		private readonly IdentityContext identityContext;
		private readonly IMapper mapper;
		private readonly UserManager<RegisterUser> userManager;
		private readonly IJWT_ITI jWT;

		public AccountITIController(IdentityContext identityContext,IMapper Mapper,UserManager<RegisterUser> userManager,IJWT_ITI jWT)
		{
			this.identityContext = identityContext;
			mapper = Mapper;
			this.userManager = userManager;
			this.jWT = jWT;
		}
		// عاوز اجيب داتا من الفرونت واحفظها ف ال داتا بيز 
		[HttpPost]
		// IActionResult علشان مش هترجع حاجة معينه !
		public async Task<ActionResult<RegisterResultDto>> RegisterAsync(RegisterDtoITI registerDto)
		{
			RegisterResultDto registerResultDto = new();
			var RegisterUser= this.mapper.Map<RegisterUser>(registerDto);
			// عاوز اعمل هاش للباس بقي
			var result=await this.userManager.CreateAsync(RegisterUser,registerDto.Password);
			if (!result.Succeeded)
			{
				//return Ok(false);
				// عاوز يحطلي الايرور ف ارراي من الاسترنح 
				var errors=new List<string>();
				foreach (var error in result.Errors)
				{
					errors.Add(error.Description);
				}
				registerResultDto.errors = errors;
				return Ok(registerResultDto);
			}


			//{ return BadRequest(new HandleErrors(400, "Registeration Error!!")); }

			////string token =	await jWT.MakeToken(RegisterUser);
			//var returned = new // اننيمس اوبجيكت يعني اكنه اوبجيكت من كلاس مش موجود 
			//{
			//	email=registerDto.Email,
			//	token
			//};
			////registerResultDto.token = token;
			registerResultDto.userName = RegisterUser.UserName; // عاوز اطبع انه تمام ف لافرونت وان ال يوزر نيم بتاعه كذا 
			registerResultDto.email = RegisterUser.Email;
			return Ok(registerResultDto);

			// ده اللي انا عاوزة ان ي يرجع توكين ي يرجع فولس لل فرونت وههندل الدنيا هناك

		}

		/// اعمل لوجين بقي 
		[HttpPost("login")]
		public async Task<ActionResult<LoginResultDto>> login(UserLoginDto userLoginDto)
		{
			LoginResultDto loginResultDto = new();
			RegisterUser? registerUser;
			if (userLoginDto.UserOrEmail.Contains('@')) {

				registerUser=await this.userManager.FindByEmailAsync(userLoginDto.UserOrEmail);
				if (registerUser == null)
				{
					loginResultDto.error = "Invalid Email!";
					//return Ok(loginResultDto);
				}
			}
			else
			{
				registerUser =await this.userManager.FindByNameAsync(userLoginDto.UserOrEmail); //إنها بترجع Task تحتوي على instance من الكلاس TUser (أو null) .//Task<TUser?> FindByNameAsync(string userName)
				if (registerUser == null)
				{
					loginResultDto.error = "Invalid UserName!";
					//return Ok(loginResultDto);
				}
			}


		var validPassword = registerUser!=null&& await this.userManager.CheckPasswordAsync(registerUser, userLoginDto.Password);
		
			
			if(!validPassword)
			{

				if (loginResultDto.error == null)
				{
					loginResultDto.error = "Invalid Password!";
				}
			

				return Ok(loginResultDto);
			} // كده وضحتله مكان الخطأ بالظبط 

			//اعمل توكين 
		
				var token = await jWT.MakeToken(registerUser!);
				loginResultDto.token = token;
			
			

			return Ok(loginResultDto);
		}

	}
}
