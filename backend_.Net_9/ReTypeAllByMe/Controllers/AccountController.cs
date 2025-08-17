using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core_Layer.Interfaces;
using Core_Layer.Models.ErrorsHandle;
using Core_Layer.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository_Layer.IdentityRepository;
using ReTypeAllByMe.Dto;
using System.Security.Claims;

namespace ReTypeAllByMe.Controllers
{
	public class AccountController : BaseApiController
	{
		private readonly IJwtToken _jwtToken;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IMapper _mapper;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(IJwtToken jwtToken, UserManager<ApplicationUser> userManager, IMapper mapper, SignInManager<ApplicationUser> signInManager)
		{
			_jwtToken = jwtToken;
			_userManager = userManager;
			_mapper = mapper;
			_signInManager = signInManager;
		}
		//Register 
		[ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(HandleErrors), StatusCodes.Status400BadRequest)]

		[HttpPost("register")]
		public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
		{
			var existingUser = await _userManager.FindByEmailAsync(registerDto.Email);
			if (existingUser != null)
				return BadRequest(new HandleErrors(400, "Email already in use"));

			var applicationUser =_mapper.Map<ApplicationUser>(registerDto); // Map RegisterDto to ApplicationUser
			var Result = await _userManager.CreateAsync(applicationUser, registerDto.Password);
			if (!Result.Succeeded)
			{
				var errors = Result.Errors.Select(e => e.Description).ToList();
				return BadRequest(new ValidationErrors(errors));
			}

			var userDto = new UserDto
			{
				DisplayName = applicationUser.DisplayName,
				Email = applicationUser.Email!,
				Token = await GetToken(applicationUser)
			};
			return Ok(userDto);
		}
		[ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(HandleErrors), StatusCodes.Status401Unauthorized)]

		[HttpPost("Login")]
		public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
		{
			var user = await _userManager.FindByEmailAsync(loginDto.Email);
			if (user == null)
				return Unauthorized(new HandleErrors(401, "Email Or Password Isnt Correct!"));

			var SignInResult = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
			if (!SignInResult.Succeeded)
				return Unauthorized(new HandleErrors(401, "Email Or Password Isnt Correct!"));

			var userDto = new UserDto
			{
				DisplayName = user.DisplayName,
				Email = user.Email!,
				Token = await GetToken(user)
			};
			return Ok(userDto);
		}
		[HttpPut]
		[Authorize]
		[ProducesResponseType(typeof(AddressDto), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(HandleErrors), StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(typeof(ValidationErrors), StatusCodes.Status400BadRequest)]

		public async Task<ActionResult<AddressDto>> UpdateAddress(AddressDto addressDto)
		{
			var user = await GetUserWithAddressAsync(User);
			if (user == null)
				return Unauthorized(new HandleErrors(401));

			if (user.Address == null)
				user.Address = new Address();

			_mapper.Map(addressDto, user.Address); // هنا هياخد القيم اللي موجوده في ال ادريس dto وهيحطها ف ال يوزر دوت ادريس مع الحفاظ ع باقي قيم يوزر دوت ادريس

			var result = await _userManager.UpdateAsync(user);
			if (!result.Succeeded)
				return BadRequest(new ValidationErrors(result.Errors.Select(e => e.Description).ToList()));//Select(e => e.Description)//ال اي دي عبارة عن ايدينتتي ايرور جايه من ال ال ايرور اللي في  ايرورز اللي قبلها


			//return Updated Address From  user.Address
			var UpdatedAddressDto = _mapper.Map<AddressDto>(user.Address);
			return Ok(UpdatedAddressDto);

		}
		//get userIncludeAddress by ClaimsPrincipal User

		[ProducesResponseType(typeof(AddressDto), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(HandleErrors), StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(typeof(HandleErrors), StatusCodes.Status404NotFound)]
		[HttpGet("GetUserAddress")]
		[Authorize]
		public async Task<ActionResult<AddressDto>?> GetUserAddress()
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (string.IsNullOrEmpty(userId))
				return Unauthorized(new HandleErrors(401));

			var addressDto = await _userManager.Users
			  .Where(u => u.Id == userId)
			  .Select(u => u.Address)
			  .ProjectTo<AddressDto>(_mapper.ConfigurationProvider)
			  .FirstOrDefaultAsync();

			if (addressDto == null)
				return NotFound(new HandleErrors(404, "Address not found for this user."));

			return Ok(addressDto);

		}
		[ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
		[HttpGet("emailexists")]
		public async Task<ActionResult<bool>> CheckEmailExists(string email)
		{
			var exists = await _userManager.FindByEmailAsync(email) != null;
			return Ok(exists);
		}
		[ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(HandleErrors), StatusCodes.Status401Unauthorized)]
		[HttpGet]
		[Authorize]
		public async Task<ActionResult<UserDto>> GetCurrentUser()
		{
			var currentUser = await _userManager.GetUserAsync(User);
			if (currentUser == null)
				return Unauthorized(new HandleErrors(401, "Your User Is Not Found,Please Register And Login With New User!"));
			var userDto = new UserDto
			{
				DisplayName = currentUser.DisplayName,
				Email = currentUser.Email!,
				Token = await GetToken(currentUser)
			};
			return Ok(userDto);
		}

		///////Private Uses//////
		private async Task<ApplicationUser?> GetUserWithAddressAsync(ClaimsPrincipal User)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (string.IsNullOrEmpty(userId))
				return null;
			var user = await _userManager.Users
				.Include(u => u.Address)
				.FirstOrDefaultAsync(u => u.Id == userId);
			return user;
		}
		private async Task<string> GetToken(ApplicationUser applicationUser) => await _jwtToken.CreateTokenAsync(applicationUser);
	}
}