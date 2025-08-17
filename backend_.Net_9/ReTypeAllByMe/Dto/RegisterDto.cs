using Core_Layer.Models.Identity;
using System.ComponentModel.DataAnnotations;

namespace ReTypeAllByMe.Dto
{
	public class RegisterDto
	{
		public string DisplayName { get; set; } = null!;
		// every application user may have address
		public AddressDto? Address { get; set; }
		//Email, Password,PhoneNumber
		[EmailAddress]
		public string Email { get; set; } = null!;
		public string Password { get; set; } = null!;
		[Compare("Password", ErrorMessage = "Password and confirmation password do not match")]
		public string ConfirmPassword { get; set; } = null!;
		[Phone]
		public string phoneNumber { get; set; } = null!;
	}
}
